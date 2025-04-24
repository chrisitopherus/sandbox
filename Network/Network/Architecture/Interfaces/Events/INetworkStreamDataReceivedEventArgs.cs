using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces.Events;

/// <summary>
/// Defines the event arguments for a netwrok stream data received event.
/// </summary>
public interface INetworkStreamDataReceivedEventArgs
{
    /// <summary>
    /// Gets the data received from the network stream.
    /// </summary>
    ReadOnlyMemory<byte> Data { get; }
}
