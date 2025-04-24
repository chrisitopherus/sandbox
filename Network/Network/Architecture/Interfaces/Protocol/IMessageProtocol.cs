using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Protocol;

/// <summary>
/// Defines an asymmetric binary message protocol used for communication.
/// </summary>
/// <typeparam name="TSendMessage">The type of message that is sent.</typeparam>
/// <typeparam name="TReceiveMessage">The type of message that is received.</typeparam>
public interface IMessageProtocol<TSendMessage, TReceiveMessage> : IMessageCodec<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    /// <summary>
    /// Gets the bytes of the alive message.
    /// </summary>
    ReadOnlyMemory<byte> AliveMessageBytes { get; }

    /// <summary>
    /// Checks if <paramref name="data"/> is an alive message.
    /// </summary>
    /// <param name="data">The data to check, typically received from a network stream.</param>
    /// <returns><see langword="true"/> if <paramref name="data"/> is recognized as an alive message; otherwise <see langword="false"/>.</returns>
    bool IsAliveMessage(ReadOnlyMemory<byte> data);

    /// <summary>
    /// Gets he total size of a message from the beginning of the given <paramref name="buffer"/>.
    /// </summary>
    /// <param name="buffer">THe buffer containing a binary message.</param>
    /// <returns>The size of the <typeparamref name="TMessage"/> in bytes.</returns>
    int GetMessageSize(ReadOnlyMemory<byte> buffer);
}
