using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLI.Interfaces;

public interface ICommand : IHasIdentifiers
{
    IModifier[] Modifiers { get; }

    void Execute(CommandContext ctx);
}