using Network.Architecture.Interfaces.Protocol;
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
    private IMessageProtocol<TMessage>? messageProtocol;

    EnhancedNetworkStreamConfigurationBuilder<TMessage> WithBufferSize(int bufferSize)
    {
        this.networkBufferSize = bufferSize;
        return this;
    }

    EnhancedNetworkStreamConfigurationBuilder<TMessage> WithPollDelay(int pollDelayMs)
    {
        this.pollDelayMs = pollDelayMs;
        return this;
    }

    EnhancedNetworkStreamConfigurationBuilder<TMessage> WithProtocol(IMessageProtocol<TMessage> messageProtocol)
    {
        this.messageProtocol = messageProtocol;
        return this;
    }

    EnhancedNetworkStreamConfiguration<TMessage> Create()
    {
        if (this.messageProtocol == null)
        {
            throw new InvalidOperationException("The message protocol must be specified.");
        }

        return new EnhancedNetworkStreamConfiguration<TMessage>(this.networkBufferSize, this.pollDelayMs, this.messageProtocol);
    }
}
