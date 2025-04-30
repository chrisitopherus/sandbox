using GameStuff.Graphics.Sprites;
using GameStuff.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Ressources;

public static class RessourceRegistry
{
    private static readonly Dictionary<SpriteId, Sprite> sprites = [];
    private static readonly Dictionary<CollisionShapeId, ICollisionShape> collisionShapes = [];

    public static Sprite GetSprite(SpriteId id)
    {
        return sprites.GetValueOrDefault(id) ?? throw new ArgumentOutOfRangeException(nameof(id), $"No registered sprite found with id: {id}");
    }

    public static bool TryGetSprite(SpriteId id, out Sprite? sprite)
    {
        try
        {
            sprite = GetSprite(id);
            return true;
        }
        catch
        {
            sprite = default;
            return false;
        }
    }

    public static ICollisionShape GetCollisionShape(CollisionShapeId id)
    {
        return collisionShapes.GetValueOrDefault(id) ?? throw new ArgumentOutOfRangeException(nameof(id), $"No registered collision shape found with id: {id}");
    }

    public static bool TryGetCollisionShape(CollisionShapeId id, out ICollisionShape? collisionShape)
    {
        try
        {
            collisionShape = GetCollisionShape(id);
            return true;
        }
        catch
        {
            collisionShape = default;
            return false;
        }
    }
}
