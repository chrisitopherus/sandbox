using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces;

/// <summary>
/// Defines a arbitrary message.
/// </summary>
public interface IMessage
{
    /// <summary>
    /// Encodes the message to its binary representation.
    /// </summary>
    /// <returns>The message as bytes.</returns>
    ReadOnlyMemory<byte> Encode();
}
