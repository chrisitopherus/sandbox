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

/// <summary>
/// Represents a TCP listener that accepts incoming client connections and emits an event
/// when a new <see cref="EnhancedTcpClient{TSendMessage, TReceiveMessage}"/> connects.
/// </summary>
/// <typeparam name="TSendMessage">The type of message that can be sent to clients.</typeparam>
/// <typeparam name="TReceiveMessage">The type of message that can be received from clients.</typeparam>
public class EnhancedTcpListener<TSendMessage, TReceiveMessage> : LifecycleComponent
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// The internal TCP listener.
    /// </summary>
    private TcpListener listener;

    /// <summary>
    /// The configuration used for creating clients and binding to an endpoint.
    /// </summary>
    private readonly EnhancedTcpListenerConfiguration<TSendMessage, TReceiveMessage> configuration;

    /// <summary>
    /// The cancellation token source used to cancel the listener loop.
    /// </summary>
    private CancellationTokenSource? cancellationTokenSource;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnhancedTcpListener{TSendMessage, TReceiveMessage}"/> class.
    /// </summary>
    /// <param name="configuration">The listener configuration containing endpoint and client settings.</param>
    public EnhancedTcpListener(EnhancedTcpListenerConfiguration<TSendMessage, TReceiveMessage> configuration)
    {
        this.listener = new TcpListener(configuration.EndPoint);
        this.configuration = configuration;
        this.State = LifecycleState.Initialized;
    }

    /// <summary>
    /// Is raised when a new client connects to the listener.
    /// </summary>
    public virtual event EventHandler<EnhancedTcpListenerNewClientEventArgs<TSendMessage, TReceiveMessage>>? NewClient;

    /// <summary>
    /// Starts the TCP listener and begins accepting incoming connections in the background.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the listener is already started.</exception>
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

    /// <summary>
    /// Stops the TCP listener and cancels the background connection loop.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the listener is not in the started state.</exception>
    public override void Stop()
    {
        if (this.State == LifecycleState.Stopped)
        {
            return;
        }

        if (this.State != LifecycleState.Started)
        {
            throw new InvalidOperationException("Listener is not running.");
        }

        this.cancellationTokenSource?.Cancel();
        this.cancellationTokenSource = null;
        this.State = LifecycleState.Stopped;
    }

    /// <summary>
    /// Raises the <see cref="NewClient"/> event.
    /// </summary>
    /// <param name="e">The event arguments containing the connected client instance.</param>
    protected virtual void FireOnNewClient(EnhancedTcpListenerNewClientEventArgs<TSendMessage, TReceiveMessage> e)
    {
        this.NewClient?.Invoke(this, e);
    }

    /// <summary>
    /// Asynchronously listens for incoming TCP connections and creates client wrappers for each.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to stop the listener loop.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
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
            this.Stop();
        }
    }
}
