using Helpers.Utility.Keyboard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Core;

public class GameEngine
{
    private readonly Stack<Scene> scenes = [];
    private readonly KeyboardWatcher keyboardWatcher = new();
    private readonly Action ressourceLoader;
    private readonly int interval = 16;

    public GameEngine(Action ressourceLoader)
    {
        this.ressourceLoader = ressourceLoader;
    }

    public Scene? CurrentScene => this.scenes.Count > 0 ? this.scenes.Peek() : null;

    public void Run()
    {
        this.ressourceLoader();
        this.keyboardWatcher.KeyPressed += this.KeyboardWatcherOnKeyPressedHandler;
        this.keyboardWatcher.Start();

        Stopwatch stopwatch = Stopwatch.StartNew();
        TimeSpan lastUpdate = stopwatch.Elapsed;

        Thread.Sleep(this.interval);

        while (this.scenes.Count > 0)
        {
            TimeSpan now = stopwatch.Elapsed;
            TimeSpan deltaTime = now - lastUpdate;
            lastUpdate = now;

            this.CheckCollisions();
            this.Update(deltaTime);
            this.Render();

            Thread.Sleep(this.interval);
        }

        this.keyboardWatcher.KeyPressed -= this.KeyboardWatcherOnKeyPressedHandler;
        this.keyboardWatcher.Stop();
    }

    public void PushScene(Scene scene)
    {
        scene.Init();
        this.scenes.Push(scene);
    }

    public void PopScene()
    {
        this.scenes.Pop();
    }

    private void KeyboardWatcherOnKeyPressedHandler(object? sender, KeyboardWatcherKeyPressedEventArgs e)
    {
        foreach (Scene scene in this.scenes)
        {
            scene.HandleKeyInput(e.KeyData);

            // if scene blocks input for lower scenes -> exit
            if (scene.BlocksInput)
            {
                break;
            }
        }
    }

    private void CheckCollisions()
    {
        //
    }

    private void Update(TimeSpan deltaTime)
    {
        foreach (Scene scene in this.scenes)
        {
            scene.Update(deltaTime);

            // if scene blocks updates for lower scenes -> exit
            if (scene.BlocksUpdate)
            {
                break;
            }
        }
    }

    private void Render()
    {
        foreach (Scene scene in this.scenes)
        {
            scene.Render();

            // if scene blocks renders for lower scenes -> exit
            if (scene.BlocksRender)
            {
                break;
            }
        }
    }
}
