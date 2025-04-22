using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Utility.Extensions;

public static class MessageCodecExtensions
{
    public static IMessageCodec<TBase> Adapt<TBase, TDerived>(this IMessageCodec<TDerived> codec)
        where TDerived : TBase
    {
        return new MessageCodecAdapter<TBase, TDerived>(codec);
    }
}
