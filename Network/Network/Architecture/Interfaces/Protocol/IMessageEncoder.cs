using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

public interface IMessageEncoder<TMessage>
    where TMessage : IMessage
{
    ReadOnlyMemory<byte> Encode(TMessage message);
}
