using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

public interface IMessageDecoderRegistry<TKey, TMessage>
    where TMessage : IMessage
{
    IMessageDecoder<TMessage> GetMessageDecoder(TKey key);
    bool TryGetMessageDecoder(TKey key, out IMessageDecoder<TMessage> decoder);
}
