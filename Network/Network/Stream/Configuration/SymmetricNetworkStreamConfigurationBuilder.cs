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
/// Represents a fluent builder for creating instances of <see cref="SymmetricNetworkStreamConfiguration{TMessage}"/>,
/// where the same message type is used for both sending and receiving.
/// Inherits all configuration options from <see cref="EnhancedNetworkStreamConfigurationBuilder{TMessage, TMessage}"/>.
/// </summary>
/// <typeparam name="TMessage">The type of message used for both sending and receiving.</typeparam>
public class SymmetricNetworkStreamConfigurationBuilder<TMessage> : EnhancedNetworkStreamConfigurationBuilder<TMessage, TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SymmetricNetworkStreamConfigurationBuilder{TMessage}"/> class.
    /// </summary>
    /// <param name="messageProtocol">The symmetric message protocol used for encoding and decoding messages.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="messageProtocol"/> is null.</exception>
    public SymmetricNetworkStreamConfigurationBuilder(ISymmetricMessageProtocol<TMessage> messageProtocol)
        : base(messageProtocol)
    {
    }

    /// <summary>
    /// Resets the builder to use default configuration values:
    /// 4096-byte buffer, 100ms poll delay, and alive message filtering enabled.
    /// </summary>
    /// <returns>The current builder instance with default settings applied.</returns>
    public new SymmetricNetworkStreamConfigurationBuilder<TMessage> WithDefaults()
    {
        this.WithBufferSize(4096).WithPollDelay(100);
        this.filterAliveMessages = true;
        return this;
    }

    /// <summary>
    /// Creates a new instance of <see cref="SymmetricNetworkStreamConfiguration{TMessage}"/> based on the current builder configuration.
    /// </summary>
    /// <returns>A configured <see cref="SymmetricNetworkStreamConfiguration{TMessage}"/> instance.</returns>
    public new SymmetricNetworkStreamConfiguration<TMessage> Create()
    {
        return new SymmetricNetworkStreamConfiguration<TMessage>(
            this.networkBufferSize,
            this.pollDelayMs,
            (ISymmetricMessageProtocol<TMessage>)this.messageProtocol)
        {
            FilterAliveMessages = this.filterAliveMessages
        };
    }
}
