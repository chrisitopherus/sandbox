using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Configuration;

public class GameEngineSettings
{
    public GameEngineSettings(ConsoleSettings consoleSettings, int gameLoopIntervalMs = 16)
    {
        this.ConsoleSettings = consoleSettings;
        this.GameLoopIntervalMs = gameLoopIntervalMs;
    }

    public ConsoleSettings InitialConsoleSettings { get; } = ConsoleSettings.Capture();

    public ConsoleSettings ConsoleSettings { get; set; }

    public int GameLoopIntervalMs { get; set; }
}
