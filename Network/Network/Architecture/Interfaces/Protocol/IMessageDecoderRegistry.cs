using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

/// <summary>
/// Defines a registry for message decoders.
/// </summary>
/// <typeparam name="TKey">The key by which the decoders are registered.</typeparam>
/// <typeparam name="TMessage">The type of message that the decoders produce.</typeparam>
public interface IMessageDecoderRegistry<TKey, TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Gets a registered message decoder by <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key of the registered message decoder.</param>
    /// <returns>The registered message decoder.</returns>
    IMessageDecoder<TMessage> GetMessageDecoder(TKey key);

    /// <summary>
    /// Tries to get a registered message decoder by <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key of the registered message decoder.</param>
    /// <param name="decoder">On Success the retrieved message decoder</param>
    /// <returns><see langword="true"/> if a decoder was found; otherwise <see langword="false"/>.</returns>
    bool TryGetMessageDecoder(TKey key, out IMessageDecoder<TMessage> decoder);
}
