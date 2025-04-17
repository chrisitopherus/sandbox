using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLI.Interfaces;

namespace CLI;

public class CLIParserResult
{
    private Dictionary<IModifier, string> modifierValues;

    public CLIParserResult(ICommand command, string? commandValue, Dictionary<IModifier, string> modifierValues)
    {
        this.Command = command;
        this.CommandValue = commandValue;
        this.modifierValues = modifierValues;
    }

    public ICommand Command 
    { 
        get; 
        init;
    }

    public string? CommandValue
    {
        get;
    }

    public Dictionary<IModifier, string> ModifierValues
    {
        get
        {
            return this.modifierValues;
        }
    }
}