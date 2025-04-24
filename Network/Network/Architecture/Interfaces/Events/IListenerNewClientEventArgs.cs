using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Events;

/// <summary>
/// Defines the event arguments for when a new client connects to a listener.
/// </summary>
/// <typeparam name="TMessage">The type of message that the client uses for communication.</typeparam>
public interface IListenerNewClientEventArgs<TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Gets the client that has connected to the listener.
    /// </summary>
    IClient<TMessage> Client { get; }
}
