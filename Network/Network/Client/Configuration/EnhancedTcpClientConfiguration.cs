using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client.Configuration;

/// <summary>
/// Represents the configuration for an <see cref="EnhancedTcpClient{TSendMessage, TReceiveMessage}"/>, 
/// including message protocol, buffer sizes, polling delay, and keep-alive interval.
/// Inherits base stream configuration logic from <see cref="EnhancedNetworkStreamConfiguration{TSendMessage, TReceiveMessage}"/>.
/// </summary>
/// <typeparam name="TSendMessage">The type of message that will be sent.</typeparam>
/// <typeparam name="TReceiveMessage">The type of message that will be received.</typeparam>
public class EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> : EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnhancedTcpClientConfiguration{TSendMessage, TReceiveMessage}"/> class.
    /// </summary>
    /// <param name="messageProtocol">The protocol used to encode and decode messages.</param>
    /// <param name="keepAliveMessageIntervalMs">The interval in milliseconds between keep-alive messages. Must be non-negative.</param>
    /// <param name="networkBufferSize">The size of the buffer used to read from the network stream, in bytes. Must be non-negative.</param>
    /// <param name="pollDelayMs">The delay in milliseconds between polling attempts when the stream has no data. Must be non-negative.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="keepAliveMessageIntervalMs"/>, <paramref name="networkBufferSize"/>, or <paramref name="pollDelayMs"/> is less than 0.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="messageProtocol"/> is null.</exception>
    public EnhancedTcpClientConfiguration(
        IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol,
        int keepAliveMessageIntervalMs,
        int networkBufferSize,
        int pollDelayMs)
        : base(networkBufferSize, pollDelayMs, messageProtocol)
    {
        Validator.NotLessThan(keepAliveMessageIntervalMs, 0, nameof(keepAliveMessageIntervalMs));
        this.KeepAliveMessageIntervalMs = keepAliveMessageIntervalMs;
    }

    /// <summary>
    /// Gets the interval in milliseconds between keep-alive messages.
    /// </summary>
    public int KeepAliveMessageIntervalMs { get; }

    /// <summary>
    /// Creates a default configuration with a 4096-byte buffer, 100ms poll delay, and 1000ms keep-alive interval.
    /// </summary>
    /// <param name="protocol">The protocol to use for encoding and decoding messages.</param>
    /// <returns>A default-initialized <see cref="EnhancedTcpClientConfiguration{TSendMessage, TReceiveMessage}"/> instance.</returns>
    public new static EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> CreateDefault(IMessageProtocol<TSendMessage, TReceiveMessage> protocol)
    {
        return new EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage>(protocol, 1000, 4096, 100);
    }
}
