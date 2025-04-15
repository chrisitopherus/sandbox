using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces;

public interface IMessageSender<TMessage>
{
    void Send(TMessage message);

    Task SendAsync(TMessage message, CancellationToken cancellationToken = default);
}
