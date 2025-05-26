using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

/// <summary>
/// Indicates whether an object requires re-rendering or update due to changes.
/// </summary>
public interface IDirty
{
    /// <summary>
    /// Gets a value indicating whether the object is marked as dirty.
    /// </summary>
    bool IsDirty { get; }

    /// <summary>
    /// Clears the dirty flag, marking the object as clean.
    /// </summary>
    void ClearDirty();
}
