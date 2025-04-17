using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Exceptions;

public class CLIUnknownCommandException : Exception
{
    public CLIUnknownCommandException(string cmdName, string message)
        : base(message)
    {
        this.CmdName = cmdName;
    }

    public string CmdName { get; }
}
