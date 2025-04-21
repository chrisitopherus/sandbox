using ConsoleWyrm.Networking.Messages.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Utility;

public static class EnumConverter
{
    public static byte ToByte<TEnum>(TEnum value) where TEnum : struct, Enum => Convert.ToByte(value);

    public static MessageType ToMessageType(byte value) => value switch
    {
        0 => MessageType.Alive,
        1 => MessageType.GameState,
        2 => MessageType.WyrmDied,
        3 => MessageType.FoodSpawned,
        4 => MessageType.FoodEaten,
        5 => MessageType.WyrmsUpdated,
        6 => MessageType.WyrmDirectionChange,
        7 => MessageType.WyrmBoostOn,
        8 => MessageType.WyrmBoostOff,
        _ => throw new ArgumentOutOfRangeException(nameof(value), $"Not a valid value.")
    };
}
