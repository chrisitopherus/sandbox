using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

/// <summary>
/// Defines an interface for rendering and unrendering objects at specific positions.
/// </summary>
/// <typeparam name="TObject">The type of object to render.</typeparam>
public interface IRenderer<TObject>
{
    /// <summary>
    /// Renders the object at the specified position.
    /// </summary>
    /// <param name="obj">The object to render.</param>
    /// <param name="position">The position where the object should be rendered.</param>
    void Render(TObject obj, ConsolePosition position);

    /// <summary>
    /// Removes the object from the console at the specified position.
    /// </summary>
    /// <param name="obj">The object to unrender.</param>
    /// <param name="position">The position to clear.</param>
    void Unrender(TObject obj, ConsolePosition position);
}
