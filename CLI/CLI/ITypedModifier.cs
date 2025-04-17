using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLI;

public interface ITypedModifier<TType> : IModifier
{
    public TType? Parse(string? value);
}