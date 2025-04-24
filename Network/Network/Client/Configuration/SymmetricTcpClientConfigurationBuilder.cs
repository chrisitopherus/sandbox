using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream.Configuration;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client.Configuration;

public class SymmetricTcpClientConfigurationBuilder<TMessage> : EnhancedTcpClientConfigurationBuilder<TMessage, TMessage>
    where TMessage : IMessage
{
    public SymmetricTcpClientConfigurationBuilder(ISymmetricMessageProtocol<TMessage> messageProtocol)
        : base(messageProtocol)
    {
    }
}
