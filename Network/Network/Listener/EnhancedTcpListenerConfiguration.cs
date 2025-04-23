using Network.Architecture.Interfaces;
using Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener;

public class EnhancedTcpListenerConfiguration<TMessage>
    where TMessage : IMessage
{
    public EnhancedTcpListenerConfiguration(IPEndPoint endPoint, EnhancedTcpClientConfiguration<TMessage> clientConfiguration)
    {
        this.EndPoint = endPoint;
        this.ClientConfiguration = clientConfiguration;
    }

    public IPEndPoint EndPoint { get; }
    public EnhancedTcpClientConfiguration<TMessage> ClientConfiguration { get; }
}
