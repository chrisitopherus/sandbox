using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Client.Configuration;
using Network.Stream.Symmetric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client.Symmetric;

/// <summary>
/// Represents a TCP client where the same message type is used for both sending and receiving messages.
/// This is a symmetric specialization of <see cref="EnhancedTcpClient{TMessage, TMessage}"/>.
/// </summary>
/// <typeparam name="TMessage">The type of message used for both sending and receiving.</typeparam>
public class SymmetricTcpClient<TMessage> : EnhancedTcpClient<TMessage, TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SymmetricTcpClient{TMessage}"/> class.
    /// </summary>
    /// <param name="client">The underlying <see cref="TcpClient"/> used for communication.</param>
    /// <param name="configuration">The symmetric configuration that defines the message protocol and settings.</param>
    public SymmetricTcpClient(TcpClient client, SymmetricTcpClientConfiguration<TMessage> configuration)
        : base(client, configuration)
    {
    }
}
