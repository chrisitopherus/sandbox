using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

public interface IMessageCodec<TSendMessage, TReceiveMessage> : IMessageEncoder<TSendMessage>, IMessageDecoder<TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
}
