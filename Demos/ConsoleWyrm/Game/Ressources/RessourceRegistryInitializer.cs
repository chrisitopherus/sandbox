using ConsoleWyrm.Game.Graphics;
using GameStuff.Graphics.Sprites;
using GameStuff.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Ressources;

public static class RessourceRegistryInitializer
{
    public static void Initialize()
    {
        RegisterSprites();
        RegisterCollisionShapes();
    }

    private static void RegisterSprites()
    {
        Sprite wyrmHead = new(["@"], ConsoleTheme.DefaultStyle);
        Sprite wyrmTail = new(["+"], ConsoleTheme.DefaultStyle);

        RessourceRegistry.Register(SpriteId.WyrmHead, wyrmHead);
        RessourceRegistry.Register(SpriteId.WyrmTail, wyrmTail);
    }

    private static void RegisterCollisionShapes()
    {
        RessourceRegistry.Register(CollisionShapeId.Point, new PointCollisionShape());
    }
}
