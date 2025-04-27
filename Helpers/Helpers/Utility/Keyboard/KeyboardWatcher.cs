using Helpers.Utility.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Keyboard;

/// <summary>
/// A lifecycle-managed component that watches for keyboard input asynchronously.
/// Raises an event when a key is pressed, including information about modifier keys (Shift, Alt, Control).
/// </summary>
public class KeyboardWatcher : LifecycleComponent
{
    /// <summary>
    /// Manages cancellation of the keyboard monitoring task.
    /// </summary>
    private CancellationTokenSource? cancellationTokenSource;

    /// <summary>
    /// Raised whenever a key is pressed.
    /// </summary>
    public event EventHandler<KeyboardWatcherKeyPressedEventArgs>? KeyPressed;

    /// <summary>
    /// Starts the keyboard watcher.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the watcher was already started.</exception>
    public override void Start()
    {
        if (this.state == LifecycleState.Started)
        {
            throw new InvalidOperationException("Keyboardwatcher was already started.");
        }

        this.State = LifecycleState.Started;
        this.cancellationTokenSource = new CancellationTokenSource();
        Task _ = Task.Run(() => this.WatchKeyboardAsync(this.cancellationTokenSource.Token));
    }

    /// <summary>
    /// Stops the keyboard watcher.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the watcher is not running.</exception>
    public override void Stop()
    {
        if (this.state == LifecycleState.Stopped)
        {
            return;
        }

        if (this.state != LifecycleState.Started)
        {
            throw new InvalidOperationException("Keyboardwatcher is not running.");
        }

        this.cancellationTokenSource?.Cancel();
        this.cancellationTokenSource = null;
        this.State = LifecycleState.Stopped;
    }

    /// <summary>
    /// Handles failures by stopping the watcher and notifying listeners.
    /// </summary>
    /// <param name="exception">The exception that caused the failure.</param>
    protected override void Fail(Exception exception)
    {
        if (this.state == LifecycleState.Stopped)
        {
            return;
        }

        if (this.state != LifecycleState.Started)
        {
            throw new InvalidOperationException("Keyboardwatcher is not running.");
        }

        this.cancellationTokenSource?.Cancel();
        this.cancellationTokenSource = null;
        this.state = LifecycleState.Stopped;
        this.FireOnStopped(exception);
    }

    /// <summary>
    /// Fires the <see cref="KeyPressed"/> event with the given event arguments.
    /// </summary>
    /// <param name="e">The key pressed event arguments.</param>
    protected virtual void FireOnKeyPressed(KeyboardWatcherKeyPressedEventArgs e)
    {
        this.KeyPressed?.Invoke(this, e);
    }

    /// <summary>
    /// Determines whether a specific modifier key is active.
    /// </summary>
    /// <param name="modifiers">The set of current modifiers.</param>
    /// <param name="modifier">The specific modifier to check.</param>
    /// <returns><see langword="true"/> if the specified modifier is active; otherwise <see langword="false"/>.</returns>
    private bool HasModifier(ConsoleModifiers modifiers, ConsoleModifiers modifier)
    {
        return (modifiers & modifier) != 0;
    }

    /// <summary>
    /// Asynchronously watches for keyboard input and fires events when keys are pressed.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous watching for key presses operation.</returns>
    private async Task WatchKeyboardAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                    ConsoleKeyData keyData = new(
                        keyInfo.Key,
                        this.HasModifier(keyInfo.Modifiers, ConsoleModifiers.Shift),
                        this.HasModifier(keyInfo.Modifiers, ConsoleModifiers.Alt),
                        this.HasModifier(keyInfo.Modifiers, ConsoleModifiers.Control));

                    this.FireOnKeyPressed(new KeyboardWatcherKeyPressedEventArgs(keyData));
                }
                else
                {
                    await Task.Delay(10, cancellationToken);
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Expected
        }
        catch (Exception exception)
        {
            if (this.State != LifecycleState.Stopped)
            {
                this.Fail(exception);
            }
        }
        finally
        {
            if (this.State != LifecycleState.Stopped)
            {
                this.Stop();
            }
        }
    }
}
