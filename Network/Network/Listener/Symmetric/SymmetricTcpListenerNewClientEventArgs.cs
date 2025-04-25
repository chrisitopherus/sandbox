using Network.Architecture.Interfaces;
using Network.Client;
using Network.Client.Symmetric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener.Symmetric;

/// <summary>
/// Provides data for an event that occurs when a new symmetric TCP client connects to a <see cref="SymmetricTcpListener{TMessage}"/>.
/// </summary>
/// <typeparam name="TMessage">The type of message used for both sending and receiving.</typeparam>
public class SymmetricTcpListenerNewClientEventArgs<TMessage> : EventArgs
    where TMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SymmetricTcpListenerNewClientEventArgs{TMessage}"/> class.
    /// </summary>
    /// <param name="client">The connected symmetric TCP client instance.</param>
    public SymmetricTcpListenerNewClientEventArgs(SymmetricTcpClient<TMessage> client)
    {
        this.Client = client;
    }

    /// <summary>
    /// Gets the newly connected <see cref="SymmetricTcpClient{TMessage}"/> instance.
    /// </summary>
    public SymmetricTcpClient<TMessage> Client { get; }
}
