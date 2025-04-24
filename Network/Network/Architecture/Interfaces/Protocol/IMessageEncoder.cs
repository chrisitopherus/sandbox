using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

/// <summary>
/// Defines a message encoder.
/// </summary>
/// <typeparam name="TMessage">The type of message that should be encoded.</typeparam>
public interface IMessageEncoder<in TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Encodes a given <paramref name="message"/> to byte memory.
    /// </summary>
    /// <param name="message">The message that should be encoded.</param>
    /// <returns>A reado-only byte memory representation of <paramref name="message"/>.</returns>
    ReadOnlyMemory<byte> Encode(TMessage message);
}
