using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client;

public class EnhancedTcpClient<TMessage> : LifecycleComponent, IMessageSender<TMessage>
{
    private readonly TcpClient client;
    private readonly EnhancedTcpClientConfiguration<TMessage> configuration;
    private readonly EnhancedNetworkStream<TMessage> networkStream;
    private CancellationTokenSource? cancellationTokenSource;

    public EnhancedTcpClient(TcpClient client, EnhancedTcpClientConfiguration<TMessage> configuration)
    {
        this.client = client;
        this.configuration = configuration;
        this.networkStream = new EnhancedNetworkStream<TMessage>(client.GetStream(), configuration);

        this.State = LifecycleState.Initialized;
    }

    public event EventHandler<EnhancedTcpClientMessageReceivedEventArgs<TMessage>>? MessageReceived;

    public void Send(TMessage message)
    {
        try
        {
            this.networkStream.Send(message);
        }
        catch
        {
            this.Stop();
        }
    }

    public async Task SendAsync(TMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            await this.networkStream.SendAsync(message, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // Expected
        }
        catch
        {
            this.Stop();
        }
    }

    public override void Start()
    {
        if (this.State == LifecycleState.Started)
        {
            throw new InvalidOperationException("Client was already started.");
        }

        this.cancellationTokenSource = new CancellationTokenSource();
        this.networkStream.Stopped += this.EnhancedNetworkStreamStoppedHandler;
        this.networkStream.DataReceived += this.EnhancedNetworkStreamDataReceivedHandler;

        this.State = LifecycleState.Started;

        Task _ = Task.Run(() => this.KeepAliveAsync(this.cancellationTokenSource.Token));
    }

    public override void Stop()
    {
        if (this.State != LifecycleState.Started)
        {
            throw new InvalidOperationException("Client is not running.");
        }

        this.networkStream.Stopped -= this.EnhancedNetworkStreamStoppedHandler;
        this.networkStream.DataReceived -= this.EnhancedNetworkStreamDataReceivedHandler;

        this.cancellationTokenSource?.Cancel();
        this.cancellationTokenSource = null;
    }

    protected virtual void FireOnMessageReceived(EnhancedTcpClientMessageReceivedEventArgs<TMessage> e)
    {
        this.MessageReceived?.Invoke(this, e);
    }

    private void EnhancedNetworkStreamStoppedHandler(object? sender, EventArgs e)
    {
        if (this.State != LifecycleState.Stopped)
        {
            this.Stop();
        }
    }

    private void EnhancedNetworkStreamDataReceivedHandler(object? sender, EnhancedNetworkStreamDataReceivedEventArgs e)
    {
        TMessage message = this.configuration.MessageProtocol.Decode(e.Data);
        this.FireOnMessageReceived(new EnhancedTcpClientMessageReceivedEventArgs<TMessage>(message));
    }

    private async Task KeepAliveAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            TMessage aliveMessage = this.configuration.MessageProtocol.CreateAliveMessage();
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000, cancellationToken);
                await this.networkStream.SendAsync(aliveMessage, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            // Expected
        }
        catch
        {
            // Exception Handling
        }
        finally
        {
            this.State = LifecycleState.Stopped;
        }
    }
}
