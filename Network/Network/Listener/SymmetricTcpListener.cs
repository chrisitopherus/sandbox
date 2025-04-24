using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Client;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener;

public class SymmetricTcpListener<TMessage> : LifecycleComponent
    where TMessage : IMessage
{
    private TcpListener listener;
    private readonly SymmetricTcpListenerConfiguration<TMessage> configuration;
    private CancellationTokenSource? cancellationTokenSource;
    public SymmetricTcpListener(SymmetricTcpListenerConfiguration<TMessage> configuration)
    {
        this.listener = new TcpListener(configuration.EndPoint);
        this.configuration = configuration;
        this.State = LifecycleState.Initialized;
    }

    public event EventHandler<SymmetricTcpListenerNewClientEventArgs<TMessage>>? NewClient;

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
    }

    protected virtual void FireOnNewClient(SymmetricTcpListenerNewClientEventArgs<TMessage> e)
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
                EnhancedTcpClient<TMessage> client = new EnhancedTcpClient<TMessage>(tcpClient, this.configuration.ClientConfiguration);
                this.FireOnNewClient(new SymmetricTcpListenerNewClientEventArgs<TMessage>(client));
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
