using Network.Architecture.Interfaces.Protocol;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream;

public class EnhancedNetworkStreamConfiguration<TMessage>
{
    public EnhancedNetworkStreamConfiguration(int networkBufferSize, int pollDelayMs, IMessageProtocol<TMessage> messageProtocol)
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

    public IMessageProtocol<TMessage> MessageProtocol { get; }
}
