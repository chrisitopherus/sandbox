using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream.Configuration;

public class SymmetricNetworkStreamConfiguration<TMessage> : EnhancedNetworkStreamConfiguration<TMessage, TMessage>
    where TMessage : IMessage
{
    public SymmetricNetworkStreamConfiguration(int networkBufferSize, int pollDelayMs, ISymmetricMessageProtocol<TMessage> messageProtocol)
        : base(networkBufferSize, pollDelayMs, messageProtocol)
    {
    }
}
