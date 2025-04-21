using Helpers.Validation;
using Network.Architecture.Interfaces.Protocol;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream;

public class EnhancedNetworkStreamConfigurationBuilder<TMessage>
{
    private int networkBufferSize = 4096;
    private int pollDelayMs = 100;
    private bool filterAliveMessages = true;
    private IMessageProtocol<TMessage> messageProtocol;

    public EnhancedNetworkStreamConfigurationBuilder(IMessageProtocol<TMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
    }

    public EnhancedNetworkStreamConfigurationBuilder<TMessage> WithBufferSize(int bufferSize)
    {
        Validator.NotLessThan(bufferSize, 0, nameof(bufferSize));
        this.networkBufferSize = bufferSize;
        return this;
    }

    public EnhancedNetworkStreamConfigurationBuilder<TMessage> WithPollDelay(int pollDelayMs)
    {
        Validator.NotLessThan(pollDelayMs, 0, nameof(pollDelayMs));
        this.pollDelayMs = pollDelayMs;
        return this;
    }

    public EnhancedNetworkStreamConfigurationBuilder<TMessage> WithProtocol(IMessageProtocol<TMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
        return this;
    }

    public EnhancedNetworkStreamConfigurationBuilder<TMessage> DeactiveAliveMessageFilter()
    {
        this.filterAliveMessages = false;
        return this;
    }

    public EnhancedNetworkStreamConfiguration<TMessage> Create()
    {
        return new EnhancedNetworkStreamConfiguration<TMessage>(this.networkBufferSize, this.pollDelayMs, this.messageProtocol) { FilterAliveMessages = this.filterAliveMessages};
    }
}
