using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream;

public class SymmetricNetworkStreamDataReceivedEventArgs : EventArgs
{
    public SymmetricNetworkStreamDataReceivedEventArgs(ReadOnlyMemory<byte> data)
    {
        this.Data = data;
    }

    public ReadOnlyMemory<byte> Data;
}
