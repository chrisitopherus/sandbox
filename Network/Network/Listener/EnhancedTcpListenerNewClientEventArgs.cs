using Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener;

public class EnhancedTcpListenerNewClientEventArgs<TMessage>
{
    public EnhancedTcpListenerNewClientEventArgs(EnhancedTcpClient<TMessage> client)
    {
        this.Client = client;
    }

    public EnhancedTcpClient<TMessage> Client { get; }
}
