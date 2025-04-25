using Network.Architecture.Interfaces;
using Network.Client;
using Network.Client.Symmetric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener;

/// <summary>
/// Provides data for an event that occurs when a new TCP client connects to an <see cref="EnhancedTcpListener{TSendMessage, TReceiveMessage}"/>.
/// </summary>
/// <typeparam name="TSendMessage">The type of message that can be sent by the connected client.</typeparam>
/// <typeparam name="TReceiveMessage">The type of message that can be received by the connected client.</typeparam>
public class EnhancedTcpListenerNewClientEventArgs<TSendMessage, TReceiveMessage> : EventArgs
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnhancedTcpListenerNewClientEventArgs{TSendMessage, TReceiveMessage}"/> class.
    /// </summary>
    /// <param name="client">The connected client instance.</param>
    public EnhancedTcpListenerNewClientEventArgs(EnhancedTcpClient<TSendMessage, TReceiveMessage> client)
    {
        this.Client = client;
    }

    /// <summary>
    /// Gets the newly connected <see cref="EnhancedTcpClient{TSendMessage, TReceiveMessage}"/> instance.
    /// </summary>
    public EnhancedTcpClient<TSendMessage, TReceiveMessage> Client { get; }
}
