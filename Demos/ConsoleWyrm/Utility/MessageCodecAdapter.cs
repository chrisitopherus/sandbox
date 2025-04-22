using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Utility;

public class MessageCodecAdapter<TBase, TDerived> : IMessageCodec<TBase>
    where TDerived : TBase
{
    public readonly IMessageCodec<TDerived> codec;

    public MessageCodecAdapter(IMessageCodec<TDerived> codec)
    {
        this.codec = codec;
    }

    public TBase Decode(ReadOnlyMemory<byte> data)
    {
        return this.codec.Decode(data);
    }

    public ReadOnlyMemory<byte> Encode(TBase message)
    {
        if (message is not TDerived)
        {
            throw new InvalidCastException("Not supported message type.");
        }

        return this.codec.Encode((TDerived)message);
    }
}
