using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages.Data;

public enum MessageType : byte
{
    // Shared

    /// <summary>
    /// Shared.
    /// </summary>
    Alive,

    // Server to Client

    /// <summary>
    /// Server to Client.
    /// </summary>
    WyrmDied,

    /// <summary>
    /// Server to Client.
    /// add different kind of food, like steroid (red), pickle(green), esmacho(either rgb or blue)
    /// </summary>
    FoodSpawned,

    /// <summary>
    /// Server to Client.
    /// </summary>
    FoodEaten,

    /// <summary>
    /// Server to Client.
    /// </summary>
    WyrmsUpdated,

    // Client to Server

    /// <summary>
    /// Client to Server.
    /// </summary>
    WyrmDirectionChange,

    /// <summary>
    /// Client to Server.
    /// </summary>
    WyrmBoostOn,

    /// <summary>
    /// Client to Server.
    /// </summary>
    WyrmBoostOff,
}
