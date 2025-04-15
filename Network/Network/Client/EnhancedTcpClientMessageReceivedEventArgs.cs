using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client;

public class EnhancedTcpClientMessageReceivedEventArgs<TMessage> : EventArgs
{
    public EnhancedTcpClientMessageReceivedEventArgs(TMessage message)
    {
        this.Message = message;
    }

    public TMessage Message { get; }
}
