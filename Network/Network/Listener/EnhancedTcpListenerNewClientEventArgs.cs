using Network.Architecture.Interfaces;
using Network.Client;
using Network.Client.Symmetric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener;

public class EnhancedTcpListenerNewClientEventArgs<TSendMessage, TReceiveMessage> : EventArgs
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    public EnhancedTcpListenerNewClientEventArgs(EnhancedTcpClient<TSendMessage, TReceiveMessage> client)
    {
        this.Client = client;
    }

    public EnhancedTcpClient<TSendMessage, TReceiveMessage> Client { get; }
}
