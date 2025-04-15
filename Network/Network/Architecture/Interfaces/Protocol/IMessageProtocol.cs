using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

public interface IMessageProtocol<TMessage> : IMessageCodec<TMessage>
{
    ReadOnlyMemory<byte> AliveMessageBytes { get; }

    TMessage CreateAliveMessage();

    bool IsAliveMessage(ReadOnlyMemory<byte> data);
}
