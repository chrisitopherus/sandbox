using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream.Configuration;

/// <summary>
/// Represents the configuration for an <see cref="EnhancedNetworkStream{TSendMessage, TReceiveMessage}"/>,
/// including buffer sizes, polling delay, and the message protocol to use.
/// </summary>
/// <typeparam name="TSendMessage">The type of message that will be sent through the stream.</typeparam>
/// <typeparam name="TReceiveMessage">The type of message that will be received through the stream.</typeparam>
public class EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnhancedNetworkStreamConfiguration{TSendMessage, TReceiveMessage}"/> class.
    /// </summary>
    /// <param name="networkBufferSize">The size of the buffer used when reading from the network stream, in bytes. Must be non-negative.</param>
    /// <param name="pollDelayMs">The delay in milliseconds between polling attempts when no data is available. Must be non-negative.</param>
    /// <param name="messageProtocol">The message protocol used to encode and decode messages.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="networkBufferSize"/> or <paramref name="pollDelayMs"/> is less than 0.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="messageProtocol"/> is null.</exception>
    public EnhancedNetworkStreamConfiguration(int networkBufferSize, int pollDelayMs, IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol)
    {
        Validator.NotLessThan(networkBufferSize, 0, nameof(networkBufferSize));
        Validator.NotLessThan(pollDelayMs, 0, nameof(pollDelayMs));
        Validator.NotNull(messageProtocol, nameof(messageProtocol));

        this.NetworkBufferSize = networkBufferSize;
        this.PollDelayMs = pollDelayMs;
        this.MessageProtocol = messageProtocol;
    }

    /// <summary>
    /// Gets the size of the buffer used for reading from the network stream, in bytes.
    /// </summary>
    public int NetworkBufferSize { get; }

    /// <summary>
    /// Gets the delay in milliseconds between polling attempts when the network stream has no data.
    /// </summary>
    public int PollDelayMs { get; }

    /// <summary>
    /// Gets or sets a value indicating whether "alive" messages (e.g., keep-alive packets) should be filtered out and ignored. Default is <see langword="true"/>.
    /// </summary>
    public bool FilterAliveMessages { get; set; } = true;

    /// <summary>
    /// Gets the protocol used for encoding and decoding messages.
    /// </summary>
    public IMessageProtocol<TSendMessage, TReceiveMessage> MessageProtocol { get; }

    /// <summary>
    /// Creates a default configuration with a 4096-byte buffer and a 100ms poll delay.
    /// </summary>
    /// <param name="protocol">The message protocol to use with the stream.</param>
    /// <returns>A default-initialized <see cref="EnhancedNetworkStreamConfiguration{TSendMessage, TReceiveMessage}"/> instance.</returns>
    public static EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage> CreateDefault(IMessageProtocol<TSendMessage, TReceiveMessage> protocol)
    {
        return new EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage>(4096, 100, protocol);
    }
}
