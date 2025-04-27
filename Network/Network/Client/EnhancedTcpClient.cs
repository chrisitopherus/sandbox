using Helpers.Utility.Lifecycle;
using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Client.Configuration;
using Network.Stream;
using Network.Stream.Symmetric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client;

/// <summary>
/// Represents an enhanced TCP client capable of sending and receiving messages using a defined message protocol.
/// Handles connection lifecycle, message transmission, and reception using a background polling and keep-alive mechanism.
/// </summary>
/// <typeparam name="TSendMessage">The type of message to send.</typeparam>
/// <typeparam name="TReceiveMessage">The type of message to receive.</typeparam>
public class EnhancedTcpClient<TSendMessage, TReceiveMessage> : LifecycleComponent, IMessageSender<TSendMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// The underlying TCP client used for the connection.
    /// </summary>
    private readonly TcpClient client;

    /// <summary>
    /// The configuration used to initialize the client and its message handling.
    /// </summary>
    private readonly EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> configuration;

    /// <summary>
    /// The enhanced network stream that manages message-based communication.
    /// </summary>
    private readonly EnhancedNetworkStream<TSendMessage, TReceiveMessage> networkStream;

    /// <summary>
    /// The cancellation token source used to control the background keep-alive task.
    /// </summary>
    private CancellationTokenSource? cancellationTokenSource;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnhancedTcpClient{TSendMessage, TReceiveMessage}"/> class.
    /// </summary>
    /// <param name="client">The connected <see cref="TcpClient"/> instance used for communication.</param>
    /// <param name="configuration">The configuration specifying the protocol and operational settings.</param>
    /// <exception cref="InvalidOperationException">Thrown if the provided <paramref name="client"/> is not connected.</exception>
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

    /// <summary>
    /// Is raised when a message is received from the remote endpoint.
    /// </summary>
    public event EventHandler<TcpClientMessageReceivedEventArgs<TReceiveMessage>>? MessageReceived;

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <summary>
    /// Starts the TCP client, begins receiving data and sending periodic keep-alive messages.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the client is already started.</exception>
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

    /// <summary>
    /// Stops the TCP client and terminates background tasks.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the client is not in a started state.</exception>
    public override void Stop()
    {
        if (this.State == LifecycleState.Stopped)
        {
            return;
        }

        if (this.State != LifecycleState.Started)
        {
            throw new InvalidOperationException("Client is not running.");
        }

        this.networkStream.Stopped -= this.EnhancedNetworkStreamStoppedHandler;
        this.networkStream.DataReceived -= this.EnhancedNetworkStreamDataReceivedHandler;

        this.cancellationTokenSource?.Cancel();
        this.cancellationTokenSource = null;
        this.State = LifecycleState.Stopped;
    }

    protected override void Fail(Exception exception)
    {
        if (this.state == LifecycleState.Stopped)
        {
            return;
        }

        if (this.state != LifecycleState.Started)
        {
            throw new InvalidOperationException("Client is not running.");
        }

        this.networkStream.Stopped -= this.EnhancedNetworkStreamStoppedHandler;
        this.networkStream.DataReceived -= this.EnhancedNetworkStreamDataReceivedHandler;

        this.cancellationTokenSource?.Cancel();
        this.cancellationTokenSource = null;

        // not using setter to avoid sending 2 events
        this.state = LifecycleState.Stopped;
        this.FireOnStopped(exception);
    }

    /// <summary>
    /// Fires the <see cref="MessageReceived"/> event with the given message.
    /// </summary>
    /// <param name="e">The event arguments containing the received message.</param>
    protected virtual void FireOnMessageReceived(TcpClientMessageReceivedEventArgs<TReceiveMessage> e)
    {
        this.MessageReceived?.Invoke(this, e);
    }
        
    /// <summary>
    /// Handles the network stream's <see cref="LifecycleComponent.Stopped"/> event and stops the client if not already stopped.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void EnhancedNetworkStreamStoppedHandler(object? sender, EventArgs e)
    {
        if (this.State != LifecycleState.Stopped)
        {
            this.Stop();
        }
    }

    /// <summary>
    /// Handles the ´<see cref="EnhancedNetworkStream.DataReceived"/> event of the network stream by decoding the message and firing the client event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments containing the received data.</param>
    private void EnhancedNetworkStreamDataReceivedHandler(object? sender, NetworkStreamDataReceivedEventArgs e)
    {
        TReceiveMessage message = this.configuration.MessageProtocol.Decode(e.Data);
        this.FireOnMessageReceived(new TcpClientMessageReceivedEventArgs<TReceiveMessage>(message));
    }

    /// <summary>
    /// Sends periodic "alive" messages to keep the connection open.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the keep-alive loop.</param>
    /// <returns>A task representing the background operation.</returns>
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
        catch (Exception exception)
        {
            this.Fail(exception);
        }
        finally
        {
            if (this.State != LifecycleState.Stopped)
            {
                this.Stop();
            }
        }
    }
}
