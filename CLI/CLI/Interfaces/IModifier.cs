using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLI.Interfaces
{
    public interface IModifier : IHasIdentifiers
    {
        bool IsFlag { get; }
        bool IsRequired { get; }
    }
}