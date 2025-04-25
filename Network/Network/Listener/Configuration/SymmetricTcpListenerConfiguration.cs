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
/// Represents the configuration for a <see cref="Listener.Symmetric.SymmetricTcpListener{TMessage}"/> 
/// where the same message type is used for both sending and receiving.
/// Inherits base listener configuration from <see cref="EnhancedTcpListenerConfiguration{TMessage, TMessage}"/>.
/// </summary>
/// <typeparam name="TMessage">The type of message used for both sending and receiving.</typeparam>
public class SymmetricTcpListenerConfiguration<TMessage> : EnhancedTcpListenerConfiguration<TMessage, TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SymmetricTcpListenerConfiguration{TMessage}"/> class.
    /// </summary>
    /// <param name="endPoint">The IP endpoint the listener will bind to.</param>
    /// <param name="clientConfiguration">The symmetric client configuration to apply to each accepted client.</param>
    public SymmetricTcpListenerConfiguration(IPEndPoint endPoint, SymmetricTcpClientConfiguration<TMessage> clientConfiguration)
        : base(endPoint, clientConfiguration)
    {
    }
}
