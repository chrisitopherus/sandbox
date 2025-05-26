using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

/// <summary>
/// Defines a component that can be initialized once.
/// </summary>
public interface IInitializable
{
    /// <summary>
    /// Gets a value indicating whether the component has been initialized.
    /// </summary>
    bool IsInitialized { get; }

    /// <summary>
    /// Performs initialization logic.
    /// </summary>
    void Initialize();
}
