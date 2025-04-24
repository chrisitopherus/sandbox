using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

/// <summary>
/// Defines a message codec for an asymmetric protocol.
/// </summary>
/// <typeparam name="TSendMessage">The type of message that is sent (encoded).</typeparam>
/// <typeparam name="TReceiveMessage">The type of message that is received (decoded).</typeparam>
public interface IMessageCodec<TSendMessage, TReceiveMessage> : IMessageEncoder<TSendMessage>, IMessageDecoder<TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
}
