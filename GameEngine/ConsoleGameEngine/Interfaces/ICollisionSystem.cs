using ConsoleGameEngine.Physics.Collisions.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

public interface ICollisionSystem
{
    public void Subscribe<TOtherCollidable>(ICollidable collidable, Action<TOtherCollidable> collisionHandler) where TOtherCollidable : ICollidable;

    public void Unsubscribe<TOtherCollidable>(ICollidable collidable, Action<TOtherCollidable> collisionHandler) where TOtherCollidable : ICollidable;

    public void Register(ICollidable collidable);

    public void Unregister(ICollidable collidable);
}
