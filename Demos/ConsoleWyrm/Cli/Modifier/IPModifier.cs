using CLI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Cli.Modifier;

public class IPModifier : ITypedModifier<IPAddress>
{
    public IPModifier(string[] identifiers)
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

    public string Description => "Specifies the ip.";

    public IPAddress? Parse(string? value)
    {
        if (IPAddress.TryParse(value, out IPAddress? ip))
        {
            return ip;
        }

        return null;
    }
}
