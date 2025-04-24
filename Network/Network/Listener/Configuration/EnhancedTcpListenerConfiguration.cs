using Network.Architecture.Interfaces;
using Network.Client.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener.Configuration;

public class EnhancedTcpListenerConfiguration<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    public EnhancedTcpListenerConfiguration(IPEndPoint endPoint, EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> clientConfiguration)
    {
        this.EndPoint = endPoint;
        this.ClientConfiguration = clientConfiguration;
    }

    public IPEndPoint EndPoint { get; }
    public EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> ClientConfiguration { get; }
}
