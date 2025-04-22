using ConsoleWyrm.Networking.Messages.Server;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages.Codecs.Server;

public class GameStateMessageCodec : IMessageCodec<GameStateMessage>
{
    public GameStateMessage Decode(ReadOnlyMemory<byte> data)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyMemory<byte> Encode(GameStateMessage message)
    {
        throw new NotImplementedException();
    }
}
