using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLI;

public class CommandContext
{
    private readonly Dictionary<IModifier, string> modifierValues;
    public CommandContext(Dictionary<IModifier, string> modifierValues, string? commandValue)
    {
        this.modifierValues = modifierValues;
        this.CommandValue = commandValue;
    }

    public string? CommandValue
    {
        get;
    }

    public string GetModifierValue(IModifier modifier)
    {
        throw new NotImplementedException();
    }

    public TValue GetModifierValue<TValue>(ITypedModifier<TValue> modifier)
    {
        throw new NotImplementedException();
    }
}