using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream.Configuration;

public class SymmetricNetworkStreamConfigurationBuilder<TMessage> : EnhancedNetworkStreamConfigurationBuilder<TMessage, TMessage>
    where TMessage : IMessage
{
    public SymmetricNetworkStreamConfigurationBuilder(ISymmetricMessageProtocol<TMessage> messageProtocol)
        : base(messageProtocol)
    {
    }
}
