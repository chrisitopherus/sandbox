using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream.Configuration;

public class EnhancedNetworkStreamConfigurationBuilder<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    protected int networkBufferSize = 4096;
    protected int pollDelayMs = 100;
    protected bool filterAliveMessages = true;
    private IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol;

    public EnhancedNetworkStreamConfigurationBuilder(IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
    }

    public EnhancedNetworkStreamConfigurationBuilder<TSendMessage, TReceiveMessage> WithBufferSize(int bufferSize)
    {
        Validator.NotLessThan(bufferSize, 0, nameof(bufferSize));
        this.networkBufferSize = bufferSize;
        return this;
    }

    public EnhancedNetworkStreamConfigurationBuilder<TSendMessage, TReceiveMessage> WithPollDelay(int pollDelayMs)
    {
        Validator.NotLessThan(pollDelayMs, 0, nameof(pollDelayMs));
        this.pollDelayMs = pollDelayMs;
        return this;
    }

    public EnhancedNetworkStreamConfigurationBuilder<TSendMessage, TReceiveMessage> DeactiveAliveMessageFilter()
    {
        this.filterAliveMessages = false;
        return this;
    }

    public EnhancedNetworkStreamConfigurationBuilder<TSendMessage, TReceiveMessage> WithProtocol(IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
        return this;
    }

    public EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage> Create()
    {
        return new EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage>(this.networkBufferSize, this.pollDelayMs, this.messageProtocol) { FilterAliveMessages = this.filterAliveMessages };
    }
}
