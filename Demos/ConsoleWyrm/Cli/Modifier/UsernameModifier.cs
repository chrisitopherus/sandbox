using CLI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Cli.Modifier;

public class UsernameModifier : IModifier
{
    public UsernameModifier(string[] identifiers)
    {
        this.Identifiers = identifiers;
    }

    public string[] Identifiers
    {
        get;
    }

    public bool IsFlag
    {
        get;
        set;
    }
    public bool IsRequired
    {
        get;
        set;
    }

    public string Description => "Specifies the username for the game.";
}
