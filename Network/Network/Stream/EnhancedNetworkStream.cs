using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream.Configuration;
using Network.Util;
using System.Net.Sockets;
using Helpers.Extension;

namespace Network.Stream;

/// <summary>
/// Represents an enhanced network stream which supports sending and receiving messages using a defined protocol.
/// </summary>
/// <typeparam name="TSendMessage">The type of message that can be sent.</typeparam>
/// <typeparam name="TReceiveMessage">The type of message that can be received.</typeparam>
public class EnhancedNetworkStream<TSendMessage, TReceiveMessage> : LifecycleComponent, IMessageSender<TSendMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// The underlying network stream.
    /// </summary>
    private NetworkStream stream;

    /// <summary>
    /// The configuration of the network stream.
    /// </summary>
    private EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage> configuration;

    /// <summary>
    /// The cancellation token source used for the lifecycle operations.
    /// </summary>
    private CancellationTokenSource? cancellationTokenSource;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnhancedNetworkStream{TSendMessage, TReceiveMessage}"/> class.
    /// </summary>
    /// <param name="networkStream">The network stream to communicate over.</param>
    /// <param name="configuration">The configuration for the stream and message protocol.</param>
    public EnhancedNetworkStream(NetworkStream networkStream, EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage> configuration)
    {
        this.stream = networkStream;
        this.configuration = configuration;
        this.State = LifecycleState.Initialized;
    }

    /// <summary>
    /// Is raised when a message is received from the network stream.
    /// </summary>
    public event EventHandler<NetworkStreamDataReceivedEventArgs>? DataReceived;

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <exception cref="InvalidOperationException">Is raised if the network stream was already started.</exception>
    public override void Start()
    {
        if (this.state == LifecycleState.Started)
        {
            throw new InvalidOperationException("Network stream was already started.");
        }

        this.State = LifecycleState.Started;
        this.cancellationTokenSource = new CancellationTokenSource();
        Task _ = Task.Run(() => this.PollForDataAsync(this.cancellationTokenSource.Token));
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <exception cref="InvalidOperationException">Is raised if the network stream is not running.</exception>
    public override void Stop()
    {
        if (this.state != LifecycleState.Started)
        {
            throw new InvalidOperationException("Network stream is not running.");
        }

        this.cancellationTokenSource?.Cancel();
        this.cancellationTokenSource = null;
        this.State = LifecycleState.Stopped;
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void Send(TSendMessage message)
    {
        try
        {
            ReadOnlySpan<byte> encodedMessage = this.configuration.MessageProtocol.Encode(message).Span;
            this.stream.Write(encodedMessage);
        }
        catch
        {
            Stop();
        }
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public async Task SendAsync(TSendMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            ReadOnlyMemory<byte> encodedMessage = this.configuration.MessageProtocol.Encode(message);
            await stream.WriteAsync(encodedMessage, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // Expected
        }
        catch
        {
            Stop();
        }
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public async Task SendAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default)
    {
        try
        {
            await stream.WriteAsync(data, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // Expected
        }
        catch
        {
            Stop();
        }
    }

    /// <summary>
    /// Fíres the <see cref="DataReceived"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void FireOnDataReceived(NetworkStreamDataReceivedEventArgs e)
    {
        this.DataReceived?.Invoke(this, e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="messageSize"></param>
    /// <param name="dataBuffer"></param>
    /// <param name="networkBuffer"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private async Task<List<ReadOnlyMemory<byte>>> ExtractAllMessagesAsync(MemoryStream dataBuffer, CancellationToken cancellationToken = default)
    {
        List<ReadOnlyMemory<byte>> messages = [];
        byte[] internalBuffer = dataBuffer.GetBuffer();
        int length = dataBuffer.Length.ConvertToInt();
        int offset = 0;

        while (offset < length)
        {
            ReadOnlyMemory<byte> currentData = internalBuffer.AsMemory(offset, length - offset);

            if (!this.configuration.MessageProtocol.TryGetMessageSize(currentData, out int messageSize))
            {
                // Not enough data to determine message size
                break;
            }

            if (currentData.Length < messageSize)
            {
                // Not enough data for message
                break;
            }

            ReadOnlyMemory<byte> fullMessage = currentData[..messageSize];
            messages.Add(fullMessage);
            offset += messageSize;
        }

        // Write remaining data into a new memory stream
        int remaining = length - offset;
        dataBuffer.SetLength(0);
        if (remaining > 0)
        {
            await dataBuffer.WriteAsync(internalBuffer.AsMemory(offset, remaining), cancellationToken);
        }

        return messages;
    }

    /// <summary>
    ///  
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task PollForDataAsync(CancellationToken cancellationToken = default)
    {
        Memory<byte> buffer = new byte[this.configuration.NetworkBufferSize];
        using MemoryStream dataBuffer = new();

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!this.stream.DataAvailable)
                {
                    await Task.Delay(this.configuration.PollDelayMs, cancellationToken);
                    continue;
                }

                int readBytesCount = await this.stream.ReadAsync(buffer, cancellationToken);
                if (readBytesCount == 0)
                {
                    // remote side has closed the connection
                    break;
                }

                await dataBuffer.WriteAsync(buffer[0..readBytesCount], cancellationToken);

                List<ReadOnlyMemory<byte>> messages = await this.ExtractAllMessagesAsync(dataBuffer, cancellationToken);

                foreach (var message in messages)
                {
                    if (!this.configuration.MessageProtocol.IsAliveMessage(message) || !this.configuration.FilterAliveMessages)
                    {
                        this.FireOnDataReceived(new NetworkStreamDataReceivedEventArgs(message));
                    }
                } 
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
