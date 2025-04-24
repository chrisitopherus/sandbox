using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

/// <summary>
/// Defines a message decoder.
/// </summary>
/// <typeparam name="TMessage">The type of message that is produced by the decoder.</typeparam>
public interface IMessageDecoder<out TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Decodes given <paramref name="data"/> to <typeparamref name="TMessage"/>.
    /// </summary>
    /// <param name="data">The data that should be decoded into <typeparamref name="TMessage"/>.</param>
    /// <returns>The decoded <typeparamref name="TMessage"/>.</returns>
    TMessage Decode(ReadOnlyMemory<byte> data);
}
