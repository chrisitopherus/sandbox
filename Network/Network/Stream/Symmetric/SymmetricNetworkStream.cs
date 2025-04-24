using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream.Configuration;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream.Symmetric;

public class SymmetricNetworkStream<TMessage> : EnhancedNetworkStream<TMessage, TMessage>
    where TMessage : IMessage
{
    public SymmetricNetworkStream(NetworkStream networkStream, SymmetricNetworkStreamConfiguration<TMessage> configuration)
        : base(networkStream, configuration)
    {
    }
}
