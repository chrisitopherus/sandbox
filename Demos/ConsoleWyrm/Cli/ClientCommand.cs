using CLI;
using CLI.Interfaces;
using ConsoleWyrm.Cli.Modifier;
using ConsoleWyrm.Networking.Messages;
using ConsoleWyrm.Networking.Messages.Codecs.Server;
using ConsoleWyrm.Networking.Messages.Data;
using ConsoleWyrm.Networking.Messages.Server;
using ConsoleWyrm.Utility;
using ConsoleWyrm.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Cli;

public class ClientCommand : ICommand
{
    private readonly UsernameModifier usernameModifier = new(["-n", "--name"]) { IsFlag = false, IsRequired = true};
    private readonly IPModifier ipModifer = new IPModifier(["-ip", "--address"]) { IsFlag = false, IsRequired = true };
    private readonly PortModifier portModifier = new PortModifier(["-p", "--port"]) { IsFlag = false, IsRequired = true };
    public ClientCommand(string[] identifiers)
    {
        this.Identifiers = identifiers;
    }
    public IModifier[] Modifiers => [this.usernameModifier, this.ipModifer, this.portModifier];

    public string[] Identifiers
    {
        get;
    }

    public string Description => "Connects to a game server on the given IP and port.";

    public void Execute(CommandContext ctx)
    {
        string? username = ctx.GetModifierValue(this.usernameModifier);
        int? port = ctx.GetModifierValue(this.portModifier);
        IPAddress? ip = ctx.GetModifierValue(this.ipModifer);
        MessageCodecRegistry<IMessage<IServerMessageVisitor>> codecRegistry = this.InitializeMessageCodecRegistry();
    }

    private MessageCodecRegistry<IMessage<IServerMessageVisitor>> InitializeMessageCodecRegistry()
    {
        var gameStateMessageCodec = new GameStateMessageCodec().Adapt<IMessage<IServerMessageVisitor>, GameStateMessage>();
        return new MessageCodecRegistry<IMessage<IServerMessageVisitor>>()
            .Register(MessageType.GameState, gameStateMessageCodec);
    }
}
