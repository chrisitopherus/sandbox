using Helpers.Validation;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client;

public class EnhancedTcpClientConfiguration<TMessage> : EnhancedNetworkStreamConfiguration<TMessage>
{
    public EnhancedTcpClientConfiguration(IMessageProtocol<TMessage> messageProtocol, int keepAliveMessageIntervalMs, int networkBufferSize, int pollDelayMs)
        : base(networkBufferSize, pollDelayMs, messageProtocol)
    {
        Validator.NotLessThan(keepAliveMessageIntervalMs, 0, nameof(keepAliveMessageIntervalMs));
        this.KeepAliveMessageIntervalMs = keepAliveMessageIntervalMs;
    }

    public int KeepAliveMessageIntervalMs { get; }

    public new static EnhancedTcpClientConfiguration<TMessage> CreateDefault(IMessageProtocol<TMessage> protocol)
    {
        return new EnhancedTcpClientConfiguration<TMessage>(protocol, 1000, 4096, 100);
    }
}
