using Network.Architecture.Interfaces;
using Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener;

public class SymmetricTcpListenerConfiguration<TMessage>
    where TMessage : IMessage
{
    public SymmetricTcpListenerConfiguration(IPEndPoint endPoint, SymmetricTcpClientConfiguration<TMessage> clientConfiguration)
    {
        this.EndPoint = endPoint;
        this.ClientConfiguration = clientConfiguration;
    }

    public IPEndPoint EndPoint { get; }
    public SymmetricTcpClientConfiguration<TMessage> ClientConfiguration { get; }
}
