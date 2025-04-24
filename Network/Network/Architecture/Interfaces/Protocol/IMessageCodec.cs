using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

/// <summary>
/// Defines a message codec.
/// </summary>
/// <typeparam name="TMessage">The type of message that is either encoded or decoded</typeparam>
public interface IMessageCodec<TMessage> : IMessageEncoder<TMessage>, IMessageDecoder<TMessage>
    where TMessage : IMessage
{
}
