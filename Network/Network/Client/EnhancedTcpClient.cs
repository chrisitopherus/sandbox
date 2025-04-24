using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Client.Configuration;
using Network.Stream;
using Network.Stream.Symmetric;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client;

public class EnhancedTcpClient<TSendMessage, TReceiveMessage> : LifecycleComponent, IMessageSender<TSendMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    private readonly TcpClient client;
    private readonly EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> configuration;
    private readonly EnhancedNetworkStream<TSendMessage, TReceiveMessage> networkStream;
    private CancellationTokenSource? cancellationTokenSource;

    public EnhancedTcpClient(TcpClient client, EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> configuration)
    {
        this.client = client;
        if (!client.Connected)
        {
            throw new InvalidOperationException("Client is not connected.");
        }

        this.configuration = configuration;
        this.networkStream = new EnhancedNetworkStream<TSendMessage, TReceiveMessage>(client.GetStream(), configuration);
        this.State = LifecycleState.Initialized;
    }

    public event EventHandler<TcpClientMessageReceivedEventArgs<TReceiveMessage>>? MessageReceived;

    public void Send(TSendMessage message)
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

    public async Task SendAsync(TSendMessage message, CancellationToken cancellationToken = default)
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

    public async Task SendAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default)
    {
        try
        {
            await this.networkStream.SendAsync(data, cancellationToken);
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

    protected virtual void FireOnMessageReceived(TcpClientMessageReceivedEventArgs<TReceiveMessage> e)
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

    private void EnhancedNetworkStreamDataReceivedHandler(object? sender, NetworkStreamDataReceivedEventArgs e)
    {
        TReceiveMessage message = this.configuration.MessageProtocol.Decode(e.Data);
        this.FireOnMessageReceived(new TcpClientMessageReceivedEventArgs<TReceiveMessage>(message));
    }

    private async Task KeepAliveAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            ReadOnlyMemory<byte> aliveMessageBytes = this.configuration.MessageProtocol.AliveMessageBytes;
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000, cancellationToken);
                await this.networkStream.SendAsync(aliveMessageBytes, cancellationToken);
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
