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
/// Provides a fluent API for building <see cref="EnhancedTcpClientConfiguration{TSendMessage, TReceiveMessage}"/> instances.
/// This builder allows customization of keep-alive behavior, buffer sizes, polling delay, and alive message filtering.
/// </summary>
/// <typeparam name="TSendMessage">The type of messages to be sent.</typeparam>
/// <typeparam name="TReceiveMessage">The type of messages to be received.</typeparam>
public class EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// The protocol used to encode and decode messages.
    /// </summary>
    protected IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol;

    /// <summary>
    /// The interval in milliseconds between keep-alive messages.
    /// </summary>
    protected int keepAliveMessageIntervalMs = 1000;

    /// <summary>
    /// The buffer size for the network stream in bytes.
    /// </summary>
    protected int networkBufferSize = 4096;

    /// <summary>
    /// The polling delay in milliseconds when no data is available.
    /// </summary>
    protected int pollDelayMs = 100;

    /// <summary>
    /// Indicates whether "alive" messages should be filtered out.
    /// </summary>
    protected bool filterAliveMessages = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnhancedTcpClientConfigurationBuilder{TSendMessage, TReceiveMessage}"/> class.
    /// </summary>
    /// <param name="messageProtocol">The protocol used for encoding and decoding messages.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="messageProtocol"/> is null.</exception>
    public EnhancedTcpClientConfigurationBuilder(IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
    }

    /// <summary>
    /// Sets the message protocol to be used by the configuration.
    /// </summary>
    /// <param name="messageProtocol">The protocol instance to use.</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="messageProtocol"/> is null.</exception>
    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> WithMessageProtocol(IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
        return this;
    }

    /// <summary>
    /// Sets the interval in milliseconds between keep-alive messages.
    /// </summary>
    /// <param name="keepAliveMessageIntervalMs">The interval in milliseconds. Must be non-negative.</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="keepAliveMessageIntervalMs"/> is less than 0.</exception>
    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> WithKeepAliveMessageInterval(int keepAliveMessageIntervalMs)
    {
        Validator.NotLessThan(keepAliveMessageIntervalMs, 0, nameof(keepAliveMessageIntervalMs));
        this.keepAliveMessageIntervalMs = keepAliveMessageIntervalMs;
        return this;
    }

    /// <summary>
    /// Sets the polling delay in milliseconds for the network stream when no data is available.
    /// </summary>
    /// <param name="pollDelayMs">The polling delay in milliseconds. Must be non-negative.</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="pollDelayMs"/> is less than 0.</exception>
    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> WithPollDelay(int pollDelayMs)
    {
        Validator.NotLessThan(pollDelayMs, 0, nameof(pollDelayMs));
        this.pollDelayMs = pollDelayMs;
        return this;
    }

    /// <summary>
    /// Sets the size of the network buffer in bytes.
    /// </summary>
    /// <param name="networkBufferSize">The buffer size in bytes. Must be non-negative.</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="networkBufferSize"/> is less than 0.</exception>
    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> WithNetworkBufferSize(int networkBufferSize)
    {
        Validator.NotLessThan(networkBufferSize, 0, nameof(networkBufferSize));
        this.networkBufferSize = networkBufferSize;
        return this;
    }

    /// <summary>
    /// Disables filtering of "alive" messages (such as keep-alive packets).
    /// </summary>
    /// <returns>The current builder instance.</returns>
    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> DeactiveAliveMessageFilter()
    {
        this.filterAliveMessages = false;
        return this;
    }

    /// <summary>
    /// Copies values from an existing <see cref="EnhancedNetworkStreamConfiguration{TSendMessage, TReceiveMessage}"/>.
    /// </summary>
    /// <param name="configuration">The configuration to copy values from.</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="configuration"/> is null.</exception>
    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> WithNetworkStreamConfiguration(EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage> configuration)
    {
        Validator.NotNull(configuration, nameof(configuration));
        this.messageProtocol = configuration.MessageProtocol;
        this.pollDelayMs = configuration.PollDelayMs;
        this.networkBufferSize = configuration.NetworkBufferSize;
        this.filterAliveMessages = configuration.FilterAliveMessages;
        return this;
    }

    /// <summary>
    /// Builds and returns a new instance of <see cref="EnhancedTcpClientConfiguration{TSendMessage, TReceiveMessage}"/> using the configured values.
    /// </summary>
    /// <returns>A new <see cref="EnhancedTcpClientConfiguration{TSendMessage, TReceiveMessage}"/> instance.</returns>
    public EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> Create()
    {
        return new EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage>(this.messageProtocol, this.keepAliveMessageIntervalMs, this.networkBufferSize, this.pollDelayMs) { FilterAliveMessages = this.filterAliveMessages };
    }
}
