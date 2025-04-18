using CLI.Interfaces;
using Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Cli.Modifier;

public class PortModifier : ITypedModifier<int?>
{
    public PortModifier(string[] identifiers)
    {
        this.Identifiers = identifiers;
    }

    public string[] Identifiers { get; }

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

    public string Description => "Specifies the network port.";

    public int? Parse(string? value)
    {
        if (int.TryParse(value, out int port))
        {
            Validator.NotWithin(port, IPEndPoint.MinPort, IPEndPoint.MaxPort, nameof(value));
            return port;
        }

        return null;
    }
}
