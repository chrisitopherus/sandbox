using Network.Architecture.Interfaces;
using Network.Client;
using Network.Client.Symmetric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener.Symmetric;

public class SymmetricTcpListenerNewClientEventArgs<TMessage> : EventArgs
    where TMessage : IMessage
{
    public SymmetricTcpListenerNewClientEventArgs(SymmetricTcpClient<TMessage> client)
    {
        this.Client = client;
    }

    public SymmetricTcpClient<TMessage> Client { get; }
}
