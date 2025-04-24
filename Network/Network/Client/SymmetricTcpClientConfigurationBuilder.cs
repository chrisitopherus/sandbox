using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client;

public class SymmetricTcpClientConfigurationBuilder<TMessage>
    where TMessage : IMessage
{
    private IMessageProtocol<TMessage> messageProtocol;
    private int keepAliveMessageIntervalMs = 1000;
    private int networkBufferSize = 4096;
    private int pollDelayMs = 100;
    private bool filterAliveMessages = true;

    public SymmetricTcpClientConfigurationBuilder(IMessageProtocol<TMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
    }

    public SymmetricTcpClientConfigurationBuilder<TMessage> WithMessageProtocol(IMessageProtocol<TMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
        return this;
    }

    public SymmetricTcpClientConfigurationBuilder<TMessage> WithKeepAliveMessageInterval(int keepAliveMessageIntervalMs)
    {
        Validator.NotLessThan(keepAliveMessageIntervalMs, 0, nameof(keepAliveMessageIntervalMs));
        this.keepAliveMessageIntervalMs = keepAliveMessageIntervalMs;
        return this;
    }

    public SymmetricTcpClientConfigurationBuilder<TMessage> WithPollDelay(int pollDelayMs)
    {
        Validator.NotLessThan(pollDelayMs, 0, nameof(pollDelayMs));
        this.pollDelayMs = pollDelayMs;
        return this;
    }

    public SymmetricTcpClientConfigurationBuilder<TMessage> WithNetworkBufferSize(int networkBufferSize)
    {
        Validator.NotLessThan(networkBufferSize, 0, nameof(networkBufferSize));
        this.networkBufferSize = networkBufferSize;
        return this;
    }

    public SymmetricTcpClientConfigurationBuilder<TMessage> DeactiveAliveMessageFilter()
    {
        this.filterAliveMessages = false;
        return this;
    }

    public SymmetricTcpClientConfigurationBuilder<TMessage> WithNetworkStreamConfiguration(SymmetricNetworkStreamConfiguration<TMessage> configuration)
    {
        Validator.NotNull(configuration, nameof(configuration));
        this.messageProtocol = configuration.MessageProtocol;
        this.pollDelayMs = configuration.PollDelayMs;
        this.networkBufferSize = configuration.NetworkBufferSize;
        this.filterAliveMessages = configuration.FilterAliveMessages;
        return this;
    }

    public SymmetricTcpClientConfiguration<TMessage> Create()
    {
        return new SymmetricTcpClientConfiguration<TMessage>(this.messageProtocol, this.keepAliveMessageIntervalMs, this.networkBufferSize, this.pollDelayMs) { FilterAliveMessages = this.filterAliveMessages};
    }
}
