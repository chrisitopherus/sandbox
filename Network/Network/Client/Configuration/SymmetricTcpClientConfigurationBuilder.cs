using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream.Configuration;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client.Configuration;

/// <summary>
/// Provides a fluent builder for creating instances of <see cref="SymmetricTcpClientConfiguration{TMessage}"/>.
/// This builder specializes <see cref="EnhancedTcpClientConfigurationBuilder{TMessage, TMessage}"/> by assuming
/// the same message type is used for sending and receiving.
/// </summary>
/// <typeparam name="TMessage">The type of message used for both sending and receiving.</typeparam>
public class SymmetricTcpClientConfigurationBuilder<TMessage> : EnhancedTcpClientConfigurationBuilder<TMessage, TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SymmetricTcpClientConfigurationBuilder{TMessage}"/> class.
    /// </summary>
    /// <param name="messageProtocol">The symmetric message protocol used for encoding and decoding messages.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="messageProtocol"/> is null.</exception>
    public SymmetricTcpClientConfigurationBuilder(ISymmetricMessageProtocol<TMessage> messageProtocol)
        : base(messageProtocol)
    {
    }

    /// <summary>
    /// Builds and returns a new <see cref="SymmetricTcpClientConfiguration{TMessage}"/> using the current builder settings.
    /// </summary>
    /// <returns>A configured <see cref="SymmetricTcpClientConfiguration{TMessage}"/> instance.</returns>
    public new SymmetricTcpClientConfiguration<TMessage> Create()
    {
        return new SymmetricTcpClientConfiguration<TMessage>(
            (ISymmetricMessageProtocol<TMessage>)this.messageProtocol,
            this.keepAliveMessageIntervalMs,
            this.networkBufferSize,
            this.pollDelayMs)
        {
            FilterAliveMessages = this.filterAliveMessages
        };
    }
}
