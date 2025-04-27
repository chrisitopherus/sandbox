using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream.Symmetric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream.Configuration;

/// <summary>
/// Represents the configuration for a <see cref="SymmetricNetworkStream{TMessage}"/> where the same message type
/// is used for both sending and receiving. Inherits base stream configuration logic from
/// <see cref="EnhancedNetworkStreamConfiguration{TMessage, TMessage}"/>.
/// </summary>
/// <typeparam name="TMessage">The type of the message used for both sending and receiving.</typeparam>
public class SymmetricNetworkStreamConfiguration<TMessage> : EnhancedNetworkStreamConfiguration<TMessage, TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SymmetricNetworkStreamConfiguration{TMessage}"/> class.
    /// </summary>
    /// <param name="networkBufferSize">The size of the buffer used when reading from the network stream, in bytes.</param>
    /// <param name="pollDelayMs">The delay in milliseconds between polling attempts when no data is available.</param>
    /// <param name="messageProtocol">The symmetric message protocol used for encoding and decoding messages.</param>
    public SymmetricNetworkStreamConfiguration(int networkBufferSize, int pollDelayMs, ISymmetricMessageProtocol<TMessage> messageProtocol)
        : base(networkBufferSize, pollDelayMs, messageProtocol)
    {
    }

    /// <summary>
    /// Creates a default configuration with a 4096-byte buffer and a 100ms poll delay.
    /// </summary>
    /// <param name="protocol">The symmetric message protocol to use with the stream.</param>
    /// <returns>A default-initialized <see cref="SymmetricNetworkStreamConfiguration{TMessage}"/> instance.</returns>
    public static SymmetricNetworkStreamConfiguration<TMessage> CreateDefault(ISymmetricMessageProtocol<TMessage> protocol)
    {
        return new SymmetricNetworkStreamConfiguration<TMessage>(4096, 100, protocol);
    }
}
