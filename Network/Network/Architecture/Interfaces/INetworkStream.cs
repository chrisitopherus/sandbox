using Helpers.Utility.Lifecycle;
using Network.Architecture.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces;

/// <summary>
/// Defines a network stream.
/// </summary>
/// <typeparam name="TMessage">The message type that the network stream sends.</typeparam>
public interface INetworkStream<TMessage> : ILifecycleComponent, IMessageSender<TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Is raised when the network stream received data.
    /// </summary>
    event EventHandler<INetworkStreamDataReceivedEventArgs> DataReceived;
}
