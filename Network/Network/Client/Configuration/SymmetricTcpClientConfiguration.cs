using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Client.Symmetric;

namespace Network.Client.Configuration;

/// <summary>
/// Represents the configuration for a <see cref="SymmetricTcpClient{TMessage}"/> 
/// where the same message type is used for sending and receiving. 
/// Inherits all stream and keep-alive configuration from <see cref="EnhancedTcpClientConfiguration{TMessage, TMessage}"/>.
/// </summary>
/// <typeparam name="TMessage">The type of message used for both sending and receiving.</typeparam>
public class SymmetricTcpClientConfiguration<TMessage> : EnhancedTcpClientConfiguration<TMessage, TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SymmetricTcpClientConfiguration{TMessage}"/> class.
    /// </summary>
    /// <param name="messageProtocol">The symmetric message protocol used to encode and decode messages.</param>
    /// <param name="keepAliveMessageIntervalMs">The interval in milliseconds between keep-alive messages. Must be non-negative.</param>
    /// <param name="networkBufferSize">The size of the buffer used when reading from the network stream, in bytes. Must be non-negative.</param>
    /// <param name="pollDelayMs">The delay in milliseconds between polling attempts when the stream has no data. Must be non-negative.</param>
    public SymmetricTcpClientConfiguration(ISymmetricMessageProtocol<TMessage> messageProtocol, int keepAliveMessageIntervalMs, int networkBufferSize, int pollDelayMs)
        : base(messageProtocol, keepAliveMessageIntervalMs, networkBufferSize, pollDelayMs)
    {
    }

    /// <summary>
    /// Creates a default configuration with a 4096-byte buffer, 100ms poll delay, and 1000ms keep-alive interval.
    /// </summary>
    /// <param name="protocol">The symmetric message protocol to use.</param>
    /// <returns>A default-initialized <see cref="SymmetricTcpClientConfiguration{TMessage}"/> instance.</returns>
    public static SymmetricTcpClientConfiguration<TMessage> CreateDefault(ISymmetricMessageProtocol<TMessage> protocol)
    {
        return new SymmetricTcpClientConfiguration<TMessage>(protocol, 1000, 4096, 100);
    }
}
