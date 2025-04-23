using CLI;
using CLI.Interfaces;
using ConsoleWyrm.Cli.Modifier;
using ConsoleWyrm.Networking;
using ConsoleWyrm.Networking.Messages;
using ConsoleWyrm.Networking.Messages.Client;
using ConsoleWyrm.Networking.Messages.Codecs.Client;
using ConsoleWyrm.Networking.Messages.Codecs.Server;
using ConsoleWyrm.Networking.Messages.Data;
using ConsoleWyrm.Utility;
using Helpers.Utility;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Cli;

public class ServerCommand : ICommand
{
    private readonly IPModifier ipModifer = new(["-ip", "--address"]) { IsFlag = false, IsRequired = true };
    private readonly PortModifier portModifier = new(["-p", "--port"]) { IsFlag = false, IsRequired = true };

    public ServerCommand(string[] identifiers)
    {
        this.Identifiers = identifiers;
    }

    public IModifier[] Modifiers => [this.ipModifer, this.portModifier];

    public string[] Identifiers
    {
        get;
    }

    public string Description => "Starts the game server on the given IP and port.";

    public void Execute(CommandContext ctx)
    {
        int? port = ctx.GetModifierValue(this.portModifier);
        IPAddress? ip = ctx.GetModifierValue(this.ipModifer);
        MessageDecoderRegistry<ICustomMessage<IClientMessageVisitor>> decoderRegistry = this.InitializeMessageDecoderRegistry();
        WyrmMessageProtocol<ICustomMessage<IClientMessageVisitor>> messageProtocol = new(decoderRegistry);
    }

    private MessageDecoderRegistry<ICustomMessage<IClientMessageVisitor>> InitializeMessageDecoderRegistry()
    {
        return new MessageDecoderRegistry<ICustomMessage<IClientMessageVisitor>>()
            .Register(MessageType.WyrmBoostOff, new WyrmBoostOffMessageCodec());
    }
}
