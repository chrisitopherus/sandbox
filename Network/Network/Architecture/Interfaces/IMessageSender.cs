using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces;

public interface IMessageSender<TMessage>
{
    Task Send(TMessage message, CancellationToken? cancellationToken = null);
}
