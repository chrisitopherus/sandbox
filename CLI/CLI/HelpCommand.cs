using CLI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI;

public class HelpCommand : ICommand
{
    private readonly ICommand[] otherCommands;
    public HelpCommand(string[] identifiers, ICommand[] otherCommands)
    {
        this.otherCommands = otherCommands;
        this.Identifiers = identifiers;
    }

    public IModifier[] Modifiers => [];

    public string[] Identifiers
    {
        get;
    }

    public void Execute(CommandContext ctx)
    {
        ICommand[] allCommands = [this, .. otherCommands];
        Console.Write(this.FormatHelpText(allCommands));
    }

    private string FormatHelpText(ICommand[] commands)
    {
        StringBuilder sb = new();
        sb.AppendLine("Overview of all commands:");
        sb.AppendLine();
        sb.AppendLine(this.FormatCommands(commands));
        sb.Append("Ende Gelände");

        return sb.ToString();
    }

    private string FormatCommands(ICommand[] commands)
    {
        StringBuilder sb = new();
        foreach (ICommand command in commands)
        {
            sb.AppendLine(this.FormatCommand(command));
            sb.AppendLine();
        }

        return sb.ToString();
    }

    private string FormatCommand(ICommand command)
    {
        StringBuilder sb = new();
        sb.AppendLine($"[{string.Join(", ", command.Identifiers)}]");
        sb.Append(this.FormatModifers(command.Modifiers));

        return sb.ToString();
    }

    private string FormatModifers(IModifier[] modifiers)
    {
        StringBuilder sb = new();
        foreach (IModifier modifier in modifiers)
        {
            sb.AppendLine($"\t{this.FormatModifier(modifier)}");
        }

        return sb.ToString();
    }

    private string FormatModifier(IModifier modifier)
    {
        StringBuilder sb = new();
        string modifierIdentifiers = string.Join(" | ", modifier.Identifiers).PadRight(18);
        string modifierTypeText = this.GetModifierTypeText(modifier);
        sb.Append($"{modifierIdentifiers}({modifierTypeText})");
        return sb.ToString();
    }

    private string GetModifierTypeText(IModifier modifier)
    {
        if (modifier.IsFlag)
        {
            return "Flag";
        }

        return modifier.IsRequired ? "Required" : "Optional";
    }
}
