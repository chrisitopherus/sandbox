using ConsoleWyrm.Networking.Messages.Client;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages.Codecs.Client;

public class WyrmDirectionChangeMessageCodec : ISymmetricMessageCodec<WyrmDirectionChangeMessage>
{
    public static readonly WyrmDirectionChangeMessageCodec Instance = new();

    private WyrmDirectionChangeMessageCodec() { }

    public WyrmDirectionChangeMessage Decode(ReadOnlyMemory<byte> data)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyMemory<byte> Encode(WyrmDirectionChangeMessage message)
    {
        throw new NotImplementedException();
    }
}
