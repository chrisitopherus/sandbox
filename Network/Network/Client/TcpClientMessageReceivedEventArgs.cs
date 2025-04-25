using Network.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client;

/// <summary>
/// Represents the event data for a TCP client when a message of type <typeparamref name="TReceiveMessage"/> is received.
/// </summary>
/// <typeparam name="TReceiveMessage">The type of the received message.</typeparam>
public class TcpClientMessageReceivedEventArgs<TReceiveMessage> : EventArgs
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TcpClientMessageReceivedEventArgs{TReceiveMessage}"/> class.
    /// </summary>
    /// <param name="message">The message that was received.</param>
    public TcpClientMessageReceivedEventArgs(TReceiveMessage message)
    {
        this.Message = message;
    }

    /// <summary>
    /// Gets the message that was received by the TCP client.
    /// </summary>
    public TReceiveMessage Message { get; }
}
