using ConsoleGameEngine.Core;
using ConsoleWyrm.Game.Entities;
using ConsoleWyrm.Game.Graphics;
using ConsoleWyrm.Game.Ressources;
using ConsoleWyrm.Game.Scenes;
using ConsoleWyrm.Networking.Messages.Server;
using Helpers.Utility;
using Helpers.Utility.Lifecycle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game;

public class WyrmGame
{
    private readonly GameEngine gameEngine = new(RessourceRegistryInitializer.Initialize);

    public WyrmGame()
    {

    }

    public void Start()
    {
        this.gameEngine.PushScene(new GameScene());
        this.gameEngine.Run();
    }
}
