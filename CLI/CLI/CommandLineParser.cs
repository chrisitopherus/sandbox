using CLI.Exceptions;
using CLI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLI;

public class CommandLineParser
{
    public CommandLineParser(ICommand[] commands)
    {
        this.Commands = commands;
    }

    public ICommand[] Commands { get; private set; }

    public CLIParserResult Parse(string[] args)
    {
        if (args.Length == 0)
        {
            throw new CLIParserException("No command line arguments specified.");
        }

        string cmdName = args[0];
        ICommand command = this.GetCommandByIdentifier(cmdName);

        if (args.Length == 1)
        {
            return new CLIParserResult(command, null, []);
        }

        string? commandValue = IsCommandValue(command, args[1]) ? args[1] : null;
        int startIndex = commandValue == null ? 1 : 2;
        Dictionary<IModifier, string> modifierValues = [];

        for (int i = startIndex; i < args.Length; i++)
        {
            string value = args[i];
            IModifier? currentModifier = GetByIdentifier(command.Modifiers, value);
            if (currentModifier == null)
            {
                throw new CLIInvalidSyntaxException(value, $"Encountered value without command or modifier: \"{value}\"");
            }

            if (i == args.Length - 1 && !currentModifier.IsFlag)
            {
                throw new CLIInvalidSyntaxException(value, $"Missing value for modifier: \"{value}\"");
            }

            string modifierValue = currentModifier.IsFlag ? string.Empty : args[++i];
            modifierValues.Add(currentModifier, modifierValue);
        }

        return new CLIParserResult(command, commandValue, modifierValues);
    }

    private ICommand GetCommandByIdentifier(string cmdName)
    {
        ICommand? command = GetByIdentifier(this.Commands, cmdName);
        if (command == null)
        {
            throw new CLIUnknownCommandException(cmdName, $"Command \"{cmdName}\" is not a valid command.");
        }

        return command;
    }

    private static bool IsCommandValue(ICommand command, string value)
    {
        return GetByIdentifier(command.Modifiers, value) == null;
    }

    private static T? GetByIdentifier<T>(IEnumerable<T> collection, string identifier) where T : IHasIdentifiers
    {
        return collection.FirstOrDefault((item) => item.Identifiers.Contains(identifier));
    }
}