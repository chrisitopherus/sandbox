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

public class SymmetricTcpClientConfiguration<TMessage> : EnhancedTcpClientConfiguration<TMessage, TMessage>
    where TMessage : IMessage
{
    public SymmetricTcpClientConfiguration(ISymmetricMessageProtocol<TMessage> messageProtocol, int keepAliveMessageIntervalMs, int networkBufferSize, int pollDelayMs)
        : base(messageProtocol, keepAliveMessageIntervalMs, networkBufferSize, pollDelayMs)
    {
    }

    public static SymmetricTcpClientConfiguration<TMessage> CreateDefault(ISymmetricMessageProtocol<TMessage> protocol)
    {
        return new SymmetricTcpClientConfiguration<TMessage>(protocol, 1000, 4096, 100);
    }
}
