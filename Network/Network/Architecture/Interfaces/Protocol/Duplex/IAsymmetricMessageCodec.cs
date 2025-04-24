using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol.Duplex;

public interface IAsymmetricMessageCodec<TSendMessage, TReceiveMessage> : IMessageEncoder<TSendMessage>, IMessageDecoder<TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
}
