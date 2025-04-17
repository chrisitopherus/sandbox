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
        string cmdName = args[0];
        ICommand? command = GetByIdentifier(this.Commands, cmdName);
        if (command == null)
        {
            throw new CLIUnknownCommandException(cmdName, $"Command \"{cmdName}\" is not a valid command.");
        }

        string? commandValue = default;
        Dictionary<IModifier, string> modifierValues = [];

        // TODO: Refactor loop
        for (int i = 1; i < args.Length; i++)
        {
            string value = args[i];
            IModifier? currentModifier = GetByIdentifier(command.Modifiers, value);
            if (currentModifier == null && i != 1)
            {
                throw new CLIInvalidSyntaxException(value, $"Encountered value without command or modifier: \"{value}\"");
            }
            
            if (currentModifier == null && i == 1)
            {
                // its the command value
                commandValue = value;
                continue;
            }

            // useless check, just for satisfying the compiler
            if (currentModifier == null)
            {
                throw new CLIInvalidSyntaxException(value, "Encountered value without command or modifier: \"{value}\"");
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

    private static T? GetByIdentifier<T>(IEnumerable<T> collection, string identifier) where T : IHasIdentifiers
    {
        return collection.FirstOrDefault((item) => item.Identifiers.Contains(identifier));
    }
}