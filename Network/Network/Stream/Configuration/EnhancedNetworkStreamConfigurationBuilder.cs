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
/// Represents a fluent builder for creating instances of <see cref="EnhancedNetworkStreamConfiguration{TSendMessage, TReceiveMessage}"/>.
/// </summary>
/// <typeparam name="TSendMessage">The type of message that will be sent through the stream.</typeparam>
/// <typeparam name="TReceiveMessage">The type of message that will be received through the stream.</typeparam>
public class EnhancedNetworkStreamConfigurationBuilder<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// The buffer size for the network stream in bytes.
    /// </summary>
    protected int networkBufferSize = 4096;

    /// <summary>
    /// The polling delay in milliseconds.
    /// </summary>
    protected int pollDelayMs = 100;

    /// <summary>
    /// Indicates whether to filter out alive messages.
    /// </summary>
    protected bool filterAliveMessages = true;

    /// <summary>
    /// The message protocol to be used for encoding and decoding messages.
    /// </summary>
    protected IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnhancedNetworkStreamConfigurationBuilder{TSendMessage, TReceiveMessage}"/> class.
    /// </summary>
    /// <param name="messageProtocol">The message protocol to be used for encoding and decoding messages.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="messageProtocol"/> is null.</exception>
    public EnhancedNetworkStreamConfigurationBuilder(IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
    }

    /// <summary>
    /// Sets the buffer size for the network stream.
    /// </summary>
    /// <param name="bufferSize">The size of the buffer in bytes. Must be non-negative.</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="bufferSize"/> is less than 0.</exception>
    public EnhancedNetworkStreamConfigurationBuilder<TSendMessage, TReceiveMessage> WithBufferSize(int bufferSize)
    {
        Validator.NotLessThan(bufferSize, 0, nameof(bufferSize));
        this.networkBufferSize = bufferSize;
        return this;
    }

    /// <summary>
    /// Sets the delay between polling attempts when the stream has no data.
    /// </summary>
    /// <param name="pollDelayMs">The delay in milliseconds. Must be non-negative.</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="pollDelayMs"/> is less than 0.</exception>
    public EnhancedNetworkStreamConfigurationBuilder<TSendMessage, TReceiveMessage> WithPollDelay(int pollDelayMs)
    {
        Validator.NotLessThan(pollDelayMs, 0, nameof(pollDelayMs));
        this.pollDelayMs = pollDelayMs;
        return this;
    }

    /// <summary>
    /// Disables filtering of "alive" messages (such as keep-alive packets).
    /// </summary>
    /// <returns>The current builder instance.</returns>
    public EnhancedNetworkStreamConfigurationBuilder<TSendMessage, TReceiveMessage> DeactiveAliveMessageFilter()
    {
        this.filterAliveMessages = false;
        return this;
    }

    /// <summary>
    /// Overrides the message protocol used for message encoding and decoding.
    /// </summary>
    /// <param name="messageProtocol">The message protocol to use.</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="messageProtocol"/> is null.</exception>
    public EnhancedNetworkStreamConfigurationBuilder<TSendMessage, TReceiveMessage> WithProtocol(IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
        return this;
    }

    /// <summary>
    /// Resets the builder to use default configuration values:
    /// 4096-byte buffer, 100ms poll delay, and alive message filtering enabled.
    /// </summary>
    /// <returns>The current builder instance with default settings applied.</returns>
    public EnhancedNetworkStreamConfigurationBuilder<TSendMessage, TReceiveMessage> WithDefaults()
    {
        this.WithBufferSize(4096).WithPollDelay(100);
        this.filterAliveMessages = true;
        return this;
    }

    /// <summary>
    /// Creates a new instance of <see cref="EnhancedNetworkStreamConfiguration{TSendMessage, TReceiveMessage}"/> based on the current builder configuration.
    /// </summary>
    /// <returns>A configured <see cref="EnhancedNetworkStreamConfiguration{TSendMessage, TReceiveMessage}"/> instance.</returns>
    public EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage> Create()
    {
        return new EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage>(this.networkBufferSize, this.pollDelayMs, this.messageProtocol) { FilterAliveMessages = this.filterAliveMessages };
    }
}
