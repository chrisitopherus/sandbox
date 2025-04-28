using CLI;
using CLI.Interfaces;
using ConsoleWyrm.Cli.Modifier;
using ConsoleWyrm.Networking.Messages;
using ConsoleWyrm.Networking.Messages.Codecs.Server;
using ConsoleWyrm.Networking.Messages.Data;
using ConsoleWyrm.Networking.Messages.Server;
using ConsoleWyrm.Utility.Extensions;
using ConsoleWyrm.Utility.Messages;
using Network.Architecture.Interfaces.Protocol;
using System.Net;

namespace ConsoleWyrm.Cli;

public class ClientCommand : ICommand
{
    private readonly UsernameModifier usernameModifier = new(["-n", "--name"]) { IsFlag = false, IsRequired = true};
    private readonly IPModifier ipModifer = new(["-ip", "--address"]) { IsFlag = false, IsRequired = true };
    private readonly PortModifier portModifier = new(["-p", "--port"]) { IsFlag = false, IsRequired = true };
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
        MessageDecoderRegistry<IServerMessage> decoderRegistry = this.InitializeMessageDecoderRegistry();
    }

    private MessageDecoderRegistry<IServerMessage> InitializeMessageDecoderRegistry()
    {
        return new MessageDecoderRegistry<IServerMessage>()
            .Register(MessageType.GameState, GameStateMessageCodec.Instance);
    }
}
