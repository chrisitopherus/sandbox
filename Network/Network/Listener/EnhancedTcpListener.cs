using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Client;
using Network.Listener.Configuration;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener;

public class EnhancedTcpListener<TSendMessage, TReceiveMessage> : LifecycleComponent
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    private TcpListener listener;
    private readonly EnhancedTcpListenerConfiguration<TSendMessage, TReceiveMessage> configuration;
    private CancellationTokenSource? cancellationTokenSource;
    public EnhancedTcpListener(EnhancedTcpListenerConfiguration<TSendMessage, TReceiveMessage> configuration)
    {
        this.listener = new TcpListener(configuration.EndPoint);
        this.configuration = configuration;
        this.State = LifecycleState.Initialized;
    }

    public virtual event EventHandler<EnhancedTcpListenerNewClientEventArgs<TSendMessage, TReceiveMessage>>? NewClient;

    public override void Start()
    {
        if (this.State == LifecycleState.Started)
        {
            throw new InvalidOperationException("Listener was already started.");
        }

        this.State = LifecycleState.Started;
        this.cancellationTokenSource = new CancellationTokenSource();
        Task _ = Task.Run(() => this.ListenForConnectionsAsync(this.cancellationTokenSource.Token));
    }

    public override void Stop()
    {
        if (this.State != LifecycleState.Started)
        {
            throw new InvalidOperationException("Listener is not running.");
        }

        this.cancellationTokenSource?.Cancel();
        this.cancellationTokenSource = null;
        this.State = LifecycleState.Stopped;
    }

    protected virtual void FireOnNewClient(EnhancedTcpListenerNewClientEventArgs<TSendMessage, TReceiveMessage> e)
    {
        this.NewClient?.Invoke(this, e);
    }

    private async Task ListenForConnectionsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                this.listener.Start();
                TcpClient tcpClient = await this.listener.AcceptTcpClientAsync(cancellationToken);
                EnhancedTcpClient<TSendMessage, TReceiveMessage> client = new(tcpClient, this.configuration.ClientConfiguration);
                this.FireOnNewClient(new EnhancedTcpListenerNewClientEventArgs<TSendMessage, TReceiveMessage>(client));
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
