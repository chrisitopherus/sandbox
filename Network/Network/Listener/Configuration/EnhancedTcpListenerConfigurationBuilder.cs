using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Client.Configuration;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener.Configuration;

public class EnhancedTcpListenerConfigurationBuilder<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    private int port;
    private IPAddress? ip;
    private EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage>? clientConfiguration;

    public EnhancedTcpListenerConfigurationBuilder<TSendMessage, TReceiveMessage> WithClientConfiguration(EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> clientConfiguration)
    {
        Validator.NotNull(clientConfiguration, nameof(clientConfiguration));
        this.clientConfiguration = clientConfiguration;
        return this;
    }

    public EnhancedTcpListenerConfigurationBuilder<TSendMessage, TReceiveMessage> WithIP(IPAddress ip)
    {
        Validator.NotNull(ip, nameof(ip));
        this.ip = ip;
        return this;
    }

    public EnhancedTcpListenerConfigurationBuilder<TSendMessage, TReceiveMessage> WithPort(int port)
    {
        Validator.NotLessThan(port, IPEndPoint.MinPort, nameof(port));
        Validator.NotGreaterThan(port, IPEndPoint.MaxPort, nameof(port));
        this.port = port;
        return this;
    }

    public EnhancedTcpListenerConfiguration<TSendMessage, TReceiveMessage> Create()
    {
        if (ip == null)
        {
            throw new InvalidOperationException("IP was not set.");
        }

        if (clientConfiguration == null)
        {
            throw new InvalidOperationException("Client configuration was not set.");
        }

        IPEndPoint endPoint = new IPEndPoint(ip, port);

        return new EnhancedTcpListenerConfiguration<TSendMessage, TReceiveMessage>(endPoint, clientConfiguration);
    }
}
