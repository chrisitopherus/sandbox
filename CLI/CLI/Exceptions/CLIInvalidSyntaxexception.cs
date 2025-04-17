using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Exceptions;

public class CLIInvalidSyntaxException : Exception
{
    public CLIInvalidSyntaxException(string value, string message)
        : base(message)
    {
        this.Value = value;
    }

    public string Value { get; }
}
