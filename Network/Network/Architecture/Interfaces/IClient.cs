using Helpers.Utility.Lifecycle;
using Network.Architecture.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces;

/// <summary>
/// Defines a client.
/// </summary>
/// <typeparam name="TMessage">The type of message that the client can send.</typeparam>
public interface IClient<TMessage> : ILifecycleComponent, IMessageSender<TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Is raised when the client received a message.
    /// </summary>
    event EventHandler<IClientMessageReceivedEventArgs<TMessage>> MessageReceived;
}
