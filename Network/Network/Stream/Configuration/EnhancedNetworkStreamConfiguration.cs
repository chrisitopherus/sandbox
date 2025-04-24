using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream.Configuration;

public class EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    public EnhancedNetworkStreamConfiguration(int networkBufferSize, int pollDelayMs, IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol)
    {
        Validator.NotLessThan(networkBufferSize, 0, nameof(networkBufferSize));
        Validator.NotLessThan(pollDelayMs, 0, nameof(pollDelayMs));
        Validator.NotNull(messageProtocol, nameof(messageProtocol));

        this.NetworkBufferSize = networkBufferSize;
        this.PollDelayMs = pollDelayMs;
        this.MessageProtocol = messageProtocol;
    }

    public int NetworkBufferSize { get; }

    public int PollDelayMs { get; }

    public bool FilterAliveMessages { get; set; } = true;

    public IMessageProtocol<TSendMessage, TReceiveMessage> MessageProtocol { get; }

    public static EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage> CreateDefault(IMessageProtocol<TSendMessage, TReceiveMessage> protocol)
    {
        return new EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage>(4096, 100, protocol);
    }
}
