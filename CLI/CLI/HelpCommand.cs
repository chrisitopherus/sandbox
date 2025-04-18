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
    public HelpCommand(string[] identifiers, ICommand[] otherCommands, string? description = null)
    {
        this.otherCommands = otherCommands;
        this.Identifiers = identifiers;
        this.Description = description ?? "Shows an overview of all commands and their modifiers.";
    }

    public IModifier[] Modifiers => [];

    public string[] Identifiers
    {
        get;
    }

    public string Description
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
        sb.Append(this.FormatCommands(commands));
        sb.Append("Ende Gelände");

        return sb.ToString();
    }

    private string FormatCommands(ICommand[] commands)
    {
        StringBuilder sb = new();
        int maxCommandNameLength = commands.Max((c) => $"[{string.Join(", ", c.Identifiers)}]".Length);
        foreach (ICommand command in commands)
        {
            sb.AppendLine(this.FormatCommand(command, maxCommandNameLength));
        }

        return sb.ToString();
    }

    private string FormatCommand(ICommand command, int maxCommandNameLength)
    {
        StringBuilder sb = new();
        string cmdNames = $"[{string.Join(", ", command.Identifiers)}]".PadRight(maxCommandNameLength);
        sb.AppendLine($"{cmdNames} -> {command.Description}");
        sb.AppendLine();
        sb.Append(this.FormatModifers(command.Modifiers));

        return sb.ToString();
    }

    private string FormatModifers(IModifier[] modifiers)
    {
        StringBuilder sb = new();
        if (modifiers.Length == 0)
        {
            return sb.ToString();
        }

        int maxFirstIdentifierLength = modifiers.Max((m) => m.Identifiers[0].Length);
        int maxIdentifiersLength = modifiers.Max((m) => string.Join(" | ", m.Identifiers).Length);
        foreach (IModifier modifier in modifiers)
        {
            sb.AppendLine($"\t{this.FormatModifier(modifier, maxFirstIdentifierLength, maxIdentifiersLength)}");
        }

        return sb.ToString();
    }

    private string FormatModifier(IModifier modifier, int maxFirstIdentifierLength, int maxIdentifiersLength)
    {
        StringBuilder sb = new();
        IEnumerable<string> identifiers = modifier.Identifiers.Select((identifier, index) => index == 0 ? identifier.PadRight(maxFirstIdentifierLength) : identifier);
        string modifierIdentifiers = string.Join(" | ", identifiers).PadRight(maxIdentifiersLength);
        string modifierTypeText = this.GetModifierTypeText(modifier);
        sb.Append($"({modifierTypeText}) {modifierIdentifiers} -> {modifier.Description}");
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
