using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Client.Configuration;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener.Configuration;

/// <summary>
/// Provides a fluent builder for creating instances of <see cref="EnhancedTcpListenerConfiguration{TSendMessage, TReceiveMessage}"/>.
/// Supports configuration of the endpoint and associated TCP client settings.
/// </summary>
/// <typeparam name="TSendMessage">The type of messages to be sent by the accepted clients.</typeparam>
/// <typeparam name="TReceiveMessage">The type of messages to be received from the accepted clients.</typeparam>
public class EnhancedTcpListenerConfigurationBuilder<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// The port to bind the TCP listener to.
    /// </summary>
    protected int port;

    /// <summary>
    /// The IP address to bind the TCP listener to.
    /// </summary>
    protected IPAddress? ip;

    /// <summary>
    /// The configuration to use for accepted clients.
    /// </summary>
    protected EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage>? clientConfiguration;

    /// <summary>
    /// Sets the client configuration to be used for accepted connections.
    /// </summary>
    /// <param name="clientConfiguration">The client configuration instance.</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="clientConfiguration"/> is null.</exception>
    public EnhancedTcpListenerConfigurationBuilder<TSendMessage, TReceiveMessage> WithClientConfiguration(EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> clientConfiguration)
    {
        Validator.NotNull(clientConfiguration, nameof(clientConfiguration));
        this.clientConfiguration = clientConfiguration;
        return this;
    }

    /// <summary>
    /// Sets the IP address to bind the listener to.
    /// </summary>
    /// <param name="ip">The IP address to listen on.</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="ip"/> is null.</exception>
    public EnhancedTcpListenerConfigurationBuilder<TSendMessage, TReceiveMessage> WithIP(IPAddress ip)
    {
        Validator.NotNull(ip, nameof(ip));
        this.ip = ip;
        return this;
    }

    /// <summary>
    /// Sets the port number to bind the listener to.
    /// </summary>
    /// <param name="port">The port number (between 0 and 65535).</param>
    /// <returns>The current builder instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="port"/> is outside the valid range.</exception>
    public EnhancedTcpListenerConfigurationBuilder<TSendMessage, TReceiveMessage> WithPort(int port)
    {
        Validator.NotWithin(port, IPEndPoint.MinPort, IPEndPoint.MaxPort, nameof(port));
        this.port = port;
        return this;
    }

    /// <summary>
    /// Creates a new <see cref="EnhancedTcpListenerConfiguration{TSendMessage, TReceiveMessage}"/> using the configured values.
    /// </summary>
    /// <returns>The constructed <see cref="EnhancedTcpListenerConfiguration{TSendMessage, TReceiveMessage}"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown if IP or client configuration is not set before calling <see cref="Create"/>.</exception>
    public EnhancedTcpListenerConfiguration<TSendMessage, TReceiveMessage> Create()
    {
        if (this.ip == null)
        {
            throw new InvalidOperationException("IP was not set.");
        }

        if (this.clientConfiguration == null)
        {
            throw new InvalidOperationException("Client configuration was not set.");
        }

        IPEndPoint endPoint = new IPEndPoint(this.ip, this.port);

        return new EnhancedTcpListenerConfiguration<TSendMessage, TReceiveMessage>(endPoint, this.clientConfiguration);
    }
}
