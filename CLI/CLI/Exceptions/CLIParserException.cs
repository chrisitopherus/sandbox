using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Exceptions;

public class CLIParserException : Exception
{
    public CLIParserException(string message)
        : base(message)
    {
    }
}
