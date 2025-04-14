using Network.Architecture.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces;

public interface IClient<TMessage> : ILifecycleComponent, IMessageSender<TMessage>
{
    event EventHandler<IClientMessageReceivedEventArgs<TMessage>> MessageReceived;
}
