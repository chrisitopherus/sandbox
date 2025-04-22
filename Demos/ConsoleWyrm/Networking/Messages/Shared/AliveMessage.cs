using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Data;

namespace ConsoleWyrm.Networking.Messages.Shared;

public class AliveMessage : IMessage
{
    public MessageType Type => MessageType.Alive;

    public byte Check => 69;
}
