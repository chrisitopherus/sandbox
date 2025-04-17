using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLI.Interfaces;

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

    public string? GetModifierValue(IModifier modifier)
    {
        return this.modifierValues.TryGetValue(modifier, out string? value) ? value : null;
    }

    public TValue? GetModifierValue<TValue>(ITypedModifier<TValue> modifier)
    {
        string? value = default;
        this.modifierValues.TryGetValue(modifier, out value);
        return modifier.Parse(value);
    }
}