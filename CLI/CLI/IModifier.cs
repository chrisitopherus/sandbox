using CLI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLI
{
    public interface IModifier : IHasIdentifiers
    {
        bool IsFlag { get; }
        bool IsRequired { get; }
    }
}