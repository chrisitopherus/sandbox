using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream;

/// <summary>
/// Represents the data associated with a network stream data received event.
/// </summary>
public class NetworkStreamDataReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkStreamDataReceivedEventArgs"/> class.
    /// </summary>
    /// <param name="data">The data that was received from the network stream.</param>
    public NetworkStreamDataReceivedEventArgs(ReadOnlyMemory<byte> data)
    {
        Data = data;
    }

    /// <summary>
    /// Gets the data that was received from the network stream.
    /// </summary>
    public ReadOnlyMemory<byte> Data;
}
