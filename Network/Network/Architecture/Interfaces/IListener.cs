using Network.Architecture.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces;

/// <summary>
/// Defines a listener for client connections.
/// </summary>
/// <typeparam name="TMessage">The type of message that the connected client can send.</typeparam>
public interface IListener<TMessage> : ILifecycleComponent
    where TMessage : IMessage
{
    /// <summary>
    /// Is raised when a new client connected.
    /// </summary>
    event EventHandler<IListenerNewClientEventArgs<TMessage>> NewClient;
}
