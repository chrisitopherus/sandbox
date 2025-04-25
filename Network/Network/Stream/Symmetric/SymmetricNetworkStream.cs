using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream.Configuration;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream.Symmetric;

/// <summary>
/// Represents a network stream that uses the same message type for sending and receiving.
/// 
/// This is useful for protocols where the request and response types are identical,
/// such as in symmetric peer-to-peer communication.
/// </summary>
/// <typeparam name="TMessage">The type of the message used for both sending and receiving.</typeparam>
public class SymmetricNetworkStream<TMessage> : EnhancedNetworkStream<TMessage, TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SymmetricNetworkStream{TMessage}"/> class.
    /// </summary>
    /// <param name="networkStream">The network stream used for communication.</param>
    /// <param name="configuration">The configuration object specifying message handling and protocol settings.</param>
    public SymmetricNetworkStream(NetworkStream networkStream, SymmetricNetworkStreamConfiguration<TMessage> configuration)
        : base(networkStream, configuration)
    {
    }
}
