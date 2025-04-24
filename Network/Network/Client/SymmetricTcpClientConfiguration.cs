using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client;

public class SymmetricTcpClientConfiguration<TMessage> : SymmetricNetworkStreamConfiguration<TMessage>
    where TMessage : IMessage
{
    public SymmetricTcpClientConfiguration(ISymmetricMessageProtocol<TMessage> messageProtocol, int keepAliveMessageIntervalMs, int networkBufferSize, int pollDelayMs)
        : base(networkBufferSize, pollDelayMs, messageProtocol)
    {
        Validator.NotLessThan(keepAliveMessageIntervalMs, 0, nameof(keepAliveMessageIntervalMs));
        this.KeepAliveMessageIntervalMs = keepAliveMessageIntervalMs;
    }

    public int KeepAliveMessageIntervalMs { get; }

    public new static SymmetricTcpClientConfiguration<TMessage> CreateDefault(ISymmetricMessageProtocol<TMessage> protocol)
    {
        return new SymmetricTcpClientConfiguration<TMessage>(protocol, 1000, 4096, 100);
    }
}
