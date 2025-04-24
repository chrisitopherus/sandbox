using Network.Architecture.Interfaces;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream.Duplex;

public class AsymmetricNetworkStream<TSendMessage, TReceiveMessage> : LifecycleComponent, IMessageSender<TSendMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    private NetworkStream stream;
    // configuration

    private CancellationTokenSource? cancellationTokenSource;

    // add configuration - two seperate MessageProtocols or 1 encoder and 1 decoder

    public AsymmetricNetworkStream(NetworkStream networkStream)
    {
        this.stream = networkStream;
    }

    // add data received event

    public override void Start()
    {
        throw new NotImplementedException();
    }

    public override void Stop()
    {
        throw new NotImplementedException();
    }


    public void Send(TSendMessage message)
    {
        throw new NotImplementedException();
    }

    public Task SendAsync(TSendMessage message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SendAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    // add async polling
}
