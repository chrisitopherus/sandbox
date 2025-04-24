using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Client.Configuration;
using Network.Stream.Symmetric;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client.Symmetric;

public class SymmetricTcpClient<TMessage> : EnhancedTcpClient<TMessage, TMessage>
    where TMessage : IMessage
{
    public SymmetricTcpClient(TcpClient client, SymmetricTcpClientConfiguration<TMessage> configuration)
        : base(client, configuration)
    {
    }
}
