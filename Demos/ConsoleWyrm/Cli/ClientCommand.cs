using CLI;
using CLI.Interfaces;
using ConsoleWyrm.Cli.Modifier;
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

    public void Execute(CommandContext ctx)
    {
        string? username = ctx.GetModifierValue(this.usernameModifier);
        int? port = ctx.GetModifierValue(this.portModifier);
        IPAddress? ip = ctx.GetModifierValue(this.ipModifer);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Join(", ", this.Identifiers));
        if (this.Modifiers.Length > 0)
        {
            sb.AppendLine();
        }

        foreach (var modifier in this.Modifiers)
        {
            sb.AppendLine($"\t{modifier.ToString()}");
        }

        return sb.ToString();
    }
}
