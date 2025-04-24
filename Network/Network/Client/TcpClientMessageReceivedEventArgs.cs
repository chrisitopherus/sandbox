using Network.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client;

public class TcpClientMessageReceivedEventArgs<TReceiveMessage> : EventArgs
    where TReceiveMessage : IMessage
{
    public TcpClientMessageReceivedEventArgs(TReceiveMessage message)
    {
        this.Message = message;
    }

    public TReceiveMessage Message { get; }
}
