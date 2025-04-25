using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Client.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener.Configuration;

/// <summary>
/// Represents the configuration for an <see cref="Listener.EnhancedTcpListener{TSendMessage, TReceiveMessage}"/>,
/// including the endpoint to listen on and the configuration to use for each accepted client connection.
/// </summary>
/// <typeparam name="TSendMessage">The type of messages to be sent by clients.</typeparam>
/// <typeparam name="TReceiveMessage">The type of messages to be received from clients.</typeparam>
public class EnhancedTcpListenerConfiguration<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnhancedTcpListenerConfiguration{TSendMessage, TReceiveMessage}"/> class.
    /// </summary>
    /// <param name="endPoint">The IP endpoint on which the listener will accept incoming connections.</param>
    /// <param name="clientConfiguration">The configuration to apply to each client accepted by the listener.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="endPoint"/> or <paramref name="clientConfiguration"/> is null.</exception>
    public EnhancedTcpListenerConfiguration(IPEndPoint endPoint, EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> clientConfiguration)
    {
        this.EndPoint = endPoint;
        this.ClientConfiguration = clientConfiguration;
    }

    /// <summary>
    /// Gets the IP endpoint on which the listener will accept incoming connections.
    /// </summary>
    public IPEndPoint EndPoint { get; }

    /// <summary>
    /// Gets the configuration to apply to each accepted client.
    /// </summary>
    public EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> ClientConfiguration { get; }
}
