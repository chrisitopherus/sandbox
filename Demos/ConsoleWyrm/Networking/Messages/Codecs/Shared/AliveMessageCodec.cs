using ConsoleWyrm.Networking.Messages.Shared;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages.Codecs.Shared;

public class AliveMessageCodec : ISymmetricMessageCodec<AliveMessage>
{
    public AliveMessage Decode(ReadOnlyMemory<byte> data)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyMemory<byte> Encode(AliveMessage message)
    {
        throw new NotImplementedException();
    }
}
