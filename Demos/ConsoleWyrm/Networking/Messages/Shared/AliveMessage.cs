using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Data;

namespace ConsoleWyrm.Networking.Messages.Shared;

public static class AliveMessage
{
    public static MessageType Type => MessageType.Alive;

    public static byte Check => 69;

    public static bool IsAlive(ReadOnlySpan<byte> data) => throw new NotImplementedException();
}
