using Helpers.Utility.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Keyboard;

public class KeyboardWatcher : LifecycleComponent
{
    private CancellationTokenSource? cancellationTokenSource;
    public event EventHandler<KeyboardWatcherKeyPressedEventArgs>? KeyPressed;

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

    protected virtual void FireOnKeyPressed(KeyboardWatcherKeyPressedEventArgs e)
    {
        this.KeyPressed?.Invoke(this, e);
    }

    private bool HasModifier(ConsoleModifiers modifiers, ConsoleModifiers modifier)
    {
        return (modifiers & modifier) != 0;
    }

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
