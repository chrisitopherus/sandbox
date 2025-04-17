using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Interfaces;

public interface IHasIdentifiers
{
    string[] Identifiers { get; }
}
