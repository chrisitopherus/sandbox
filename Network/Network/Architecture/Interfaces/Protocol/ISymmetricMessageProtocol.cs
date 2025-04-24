using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

/// <summary>
/// Defines a symmetric binary message protocol for communication.
/// </summary>
/// <typeparam name="TMessage">The type of message that the protocol uses.</typeparam>
public interface ISymmetricMessageProtocol<TMessage> : IMessageProtocol<TMessage, TMessage>
    where TMessage : IMessage
{
}
