using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client.Configuration;

public class EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> : EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    public EnhancedTcpClientConfiguration(IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol, int keepAliveMessageIntervalMs, int networkBufferSize, int pollDelayMs)
        : base(networkBufferSize, pollDelayMs, messageProtocol)
    {
        Validator.NotLessThan(keepAliveMessageIntervalMs, 0, nameof(keepAliveMessageIntervalMs));
        this.KeepAliveMessageIntervalMs = keepAliveMessageIntervalMs;
    }

    public int KeepAliveMessageIntervalMs { get; }

    public new static EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> CreateDefault(IMessageProtocol<TSendMessage, TReceiveMessage> protocol)
    {
        return new EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage>(protocol, 1000, 4096, 100);
    }
}
