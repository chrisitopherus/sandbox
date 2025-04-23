using Network.Architecture.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces;

public interface IListener<TMessage> : ILifecycleComponent
    where TMessage : IMessage
{
    event EventHandler<IListenerNewClientEventArgs<TMessage>> NewClient;
}
