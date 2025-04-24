using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Events;

/// <summary>
/// Defines the event arguments for when a client receives a new message.
/// </summary>
/// <typeparam name="TMessage">The type of message that the client received.</typeparam>
public interface IClientMessageReceivedEventArgs<TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Gets the message that the client received.
    /// </summary>
    TMessage Message { get; }
}
