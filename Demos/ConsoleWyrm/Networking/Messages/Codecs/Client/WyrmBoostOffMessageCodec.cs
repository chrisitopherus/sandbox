using ConsoleWyrm.Networking.Messages.Client;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages.Codecs.Client;

public class WyrmBoostOffMessageCodec : IMessageCodec<WyrmBoostOffMessage>
{
    public WyrmBoostOffMessage Decode(ReadOnlyMemory<byte> data)
    {
        throw new NotImplementedException(); 
    }

    public ReadOnlyMemory<byte> Encode(WyrmBoostOffMessage message)
    {
        throw new NotImplementedException();
    }
}
