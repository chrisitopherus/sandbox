using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using Network.Stream.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Client.Configuration;

public class EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage>
    where TSendMessage : IMessage
    where TReceiveMessage : IMessage
{
    private IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol;
    private int keepAliveMessageIntervalMs = 1000;
    private int networkBufferSize = 4096;
    private int pollDelayMs = 100;
    private bool filterAliveMessages = true;

    public EnhancedTcpClientConfigurationBuilder(IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
    }

    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> WithMessageProtocol(IMessageProtocol<TSendMessage, TReceiveMessage> messageProtocol)
    {
        Validator.NotNull(messageProtocol, nameof(messageProtocol));
        this.messageProtocol = messageProtocol;
        return this;
    }

    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> WithKeepAliveMessageInterval(int keepAliveMessageIntervalMs)
    {
        Validator.NotLessThan(keepAliveMessageIntervalMs, 0, nameof(keepAliveMessageIntervalMs));
        this.keepAliveMessageIntervalMs = keepAliveMessageIntervalMs;
        return this;
    }

    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> WithPollDelay(int pollDelayMs)
    {
        Validator.NotLessThan(pollDelayMs, 0, nameof(pollDelayMs));
        this.pollDelayMs = pollDelayMs;
        return this;
    }

    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> WithNetworkBufferSize(int networkBufferSize)
    {
        Validator.NotLessThan(networkBufferSize, 0, nameof(networkBufferSize));
        this.networkBufferSize = networkBufferSize;
        return this;
    }

    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> DeactiveAliveMessageFilter()
    {
        this.filterAliveMessages = false;
        return this;
    }

    public EnhancedTcpClientConfigurationBuilder<TSendMessage, TReceiveMessage> WithNetworkStreamConfiguration(EnhancedNetworkStreamConfiguration<TSendMessage, TReceiveMessage> configuration)
    {
        Validator.NotNull(configuration, nameof(configuration));
        this.messageProtocol = configuration.MessageProtocol;
        this.pollDelayMs = configuration.PollDelayMs;
        this.networkBufferSize = configuration.NetworkBufferSize;
        this.filterAliveMessages = configuration.FilterAliveMessages;
        return this;
    }

    public EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage> Create()
    {
        return new EnhancedTcpClientConfiguration<TSendMessage, TReceiveMessage>(this.messageProtocol, this.keepAliveMessageIntervalMs, this.networkBufferSize, this.pollDelayMs) { FilterAliveMessages = this.filterAliveMessages };
    }
}
