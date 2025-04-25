using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Client.Configuration;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener.Configuration;

/// <summary>
/// Provides a fluent builder for creating instances of <see cref="SymmetricTcpListenerConfiguration{TMessage}"/>.
/// Inherits from <see cref="EnhancedTcpListenerConfigurationBuilder{TMessage, TMessage}"/> and assumes symmetric messaging.
/// </summary>
/// <typeparam name="TMessage">The type of message used for both sending and receiving.</typeparam>
public class SymmetricTcpListenerConfigurationBuilder<TMessage> : EnhancedTcpListenerConfigurationBuilder<TMessage, TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Holds the symmetric client configuration that will be applied to accepted clients.
    /// </summary>
    private SymmetricTcpClientConfiguration<TMessage>? symmetricTcpClientConfiguration;

    /// <summary>
    /// Sets the symmetric TCP client configuration to be used for all accepted connections.
    /// </summary>
    /// <param name="clientConfiguration">The symmetric client configuration.</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="clientConfiguration"/> is <see langword="null"/>.</exception>
    public SymmetricTcpListenerConfigurationBuilder<TMessage> WithClientConfiguration(SymmetricTcpClientConfiguration<TMessage> clientConfiguration)
    {
        Validator.NotNull(clientConfiguration, nameof(clientConfiguration));
        this.symmetricTcpClientConfiguration = clientConfiguration;
        return this;
    }

    /// <summary>
    /// Builds a <see cref="SymmetricTcpListenerConfiguration{TMessage}"/> using the current builder state.
    /// </summary>
    /// <returns>A fully initialized <see cref="SymmetricTcpListenerConfiguration{TMessage}"/> instance.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the IP address was not set or if the client configuration was not provided.
    /// </exception>
    public new SymmetricTcpListenerConfiguration<TMessage> Create()
    {
        if (this.ip == null)
        {
            throw new InvalidOperationException("IP was not set.");
        }

        if (this.symmetricTcpClientConfiguration == null)
        {
            throw new InvalidOperationException("Client configuration was not set.");
        }

        IPEndPoint endPoint = new(this.ip, this.port);
        return new SymmetricTcpListenerConfiguration<TMessage>(
            endPoint,
            this.symmetricTcpClientConfiguration
        );
    }
}
