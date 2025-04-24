using Network.Architecture.Interfaces;
using Network.Client.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener.Configuration;

public class SymmetricTcpListenerConfiguration<TMessage> : EnhancedTcpListenerConfiguration<TMessage, TMessage>
    where TMessage : IMessage
{
    public SymmetricTcpListenerConfiguration(IPEndPoint endPoint, SymmetricTcpClientConfiguration<TMessage> clientConfiguration)
        : base(endPoint, clientConfiguration)
    {
    }
}
