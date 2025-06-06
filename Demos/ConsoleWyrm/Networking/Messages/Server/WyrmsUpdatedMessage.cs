﻿using ConsoleWyrm.Networking.Messages.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages.Server;

public class WyrmsUpdatedMessage : IServerMessage
{
    public MessageType Type { get; } = MessageType.WyrmsUpdated;

    public void Accept(IServerMessageVisitor visitor)
    {
        visitor.Visit(this);
    }

    public ReadOnlyMemory<byte> Encode()
    {
        throw new NotImplementedException();
    }
}
