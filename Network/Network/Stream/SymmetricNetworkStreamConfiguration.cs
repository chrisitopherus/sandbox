using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream;

public class SymmetricNetworkStreamConfiguration<TMessage>
    where TMessage : IMessage
{
    public SymmetricNetworkStreamConfiguration(int networkBufferSize, int pollDelayMs, ISymmetricMessageProtocol<TMessage> messageProtocol)
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

    public ISymmetricMessageProtocol<TMessage> MessageProtocol { get; }

    public static SymmetricNetworkStreamConfiguration<TMessage> CreateDefault(ISymmetricMessageProtocol<TMessage> protocol)
    {
        return new SymmetricNetworkStreamConfiguration<TMessage>(4096, 100, protocol);
    }
}
