using Network.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client;

public class SymmetricTcpClientMessageReceivedEventArgs<TMessage> : EventArgs
    where TMessage : IMessage
{
    public SymmetricTcpClientMessageReceivedEventArgs(TMessage message)
    {
        this.Message = message;
    }

    public TMessage Message { get; }
}
