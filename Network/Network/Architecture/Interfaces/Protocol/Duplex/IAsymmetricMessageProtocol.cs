using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol.Duplex;

public interface IAsymmetricMessageProtocol<TSendMessage, TReceiveMessage> : IAsymmetricMessageCodec<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    ReadOnlyMemory<byte> AliveMessageBytes { get; }

    bool IsAliveMessage(ReadOnlyMemory<byte> data);

    int GetMessageSize(ReadOnlyMemory<byte> buffer);
}
