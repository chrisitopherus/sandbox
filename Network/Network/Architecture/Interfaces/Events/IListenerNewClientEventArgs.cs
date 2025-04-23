using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Events;

public interface IListenerNewClientEventArgs<TMessage>
    where TMessage : IMessage
{
    IClient<TMessage> Client { get; }
}
