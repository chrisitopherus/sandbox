using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Data;
using Network.Architecture.Interfaces;

namespace ConsoleWyrm.Networking.Messages;

/// <summary>
/// Defines a custom message.
/// </summary>
/// <remarks>
/// <para>Message Format:</para>
/// <para>- (1 Byte) Type</para>
/// <para>- (4 Bytes) Content Length (N)</para>
/// <para>- (N Bytes) Content</para>
/// </remarks>
public interface ICustomMessage : IMessage
{
    MessageType Type { get; }
}
