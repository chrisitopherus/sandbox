using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

public interface IMessageEncoder<TMessage>
{
    ReadOnlyMemory<byte> Encode(TMessage message);
}
