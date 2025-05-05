using ConsoleGameEngine.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Collisions.Detector;

public class CollisionDetector
{
    public IEnumerable<Collision> Detect(IEnumerable<GameEntity> gameEntities)
    {
        List<Collision> collisions = [];
        GameEntity[] entities = gameEntities.ToArray();

        for (int i = 0; i < entities.Length; i++)
        {
            GameEntity entityA = entities[i];

            for (int j = i + 1; j < entities.Length; j++)
            {
                GameEntity entityB = entities[j];

                if (entityA.IsDespawnRequested || entityB.IsDespawnRequested)
                {
                    Debug.Fail("Collision with an entity marked for despawn");
                }

                if (entityA.CollisionShape.Intersects(entityB.CollisionShape, entityA.Position, entityB.Position))
                {
                    collisions.Add(new Collision(entityA, entityB));
                }
            }
        }

        return collisions;
    }
}
