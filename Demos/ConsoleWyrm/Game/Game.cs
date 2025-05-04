using ConsoleGameEngine.Core;
using ConsoleWyrm.Game.Entities;
using ConsoleWyrm.Game.Graphics;
using ConsoleWyrm.Game.Ressources;
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

public class Game
{
    private readonly GameEngine gameEngine = new(RessourceRegistryInitializer.Initialize);

    public Game()
    {

    }

    public void Start()
    {
        this.gameEngine.Run();
    }
}
