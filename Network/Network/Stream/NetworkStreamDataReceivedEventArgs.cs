using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream;

public class NetworkStreamDataReceivedEventArgs : EventArgs
{
    public NetworkStreamDataReceivedEventArgs(ReadOnlyMemory<byte> data)
    {
        Data = data;
    }

    public ReadOnlyMemory<byte> Data;
}
