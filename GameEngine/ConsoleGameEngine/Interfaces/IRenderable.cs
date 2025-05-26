using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

/// <summary>
/// Represents a component that can be rendered.
/// </summary>
public interface IRenderable
{
    /// <summary>
    /// Renders the component.
    /// </summary>
    void Render();
}
