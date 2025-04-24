using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Client;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener;

public class SymmetricTcpListenerConfigurationBuilder<TMessage>
    where TMessage : IMessage
{
    private int port;
    private IPAddress? ip;
    private SymmetricTcpClientConfiguration<TMessage>? clientConfiguration;

    public SymmetricTcpListenerConfigurationBuilder<TMessage> WithClientConfiguration(SymmetricTcpClientConfiguration<TMessage> clientConfiguration)
    {
        Validator.NotNull(clientConfiguration, nameof(clientConfiguration));
        this.clientConfiguration = clientConfiguration;
        return this;
    }

    public SymmetricTcpListenerConfigurationBuilder<TMessage> WithIP(IPAddress ip)
    {
        Validator.NotNull(ip, nameof(ip));
        this.ip = ip;
        return this;
    }

    public SymmetricTcpListenerConfigurationBuilder<TMessage> WithPort(int port)
    {
        Validator.NotLessThan(port, IPEndPoint.MinPort, nameof(port));
        Validator.NotGreaterThan(port, IPEndPoint.MaxPort, nameof(port));
        this.port = port;
        return this;
    }

    public SymmetricTcpListenerConfiguration<TMessage> Create()
    {
        if (this.ip == null)
        {
            throw new InvalidOperationException("IP was not set.");
        }

        if (this.clientConfiguration == null)
        {
            throw new InvalidOperationException("Client configuration was not set.");
        }

        IPEndPoint endPoint = new IPEndPoint(this.ip, this.port);

        return new SymmetricTcpListenerConfiguration<TMessage>(endPoint, this.clientConfiguration);
    }
}
