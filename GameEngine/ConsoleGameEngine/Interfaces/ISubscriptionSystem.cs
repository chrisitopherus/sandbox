using ConsoleGameEngine.Physics.Collisions.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

public interface ISubscriptionSystem<TKey, TCallbackArgument>
{
    public void Subscribe<TOther>(TKey key, Action<TOther> handler) where TOther : ICollidable;

    public void Unsubscribe<TOther>(TKey key, Action<TOther> handler) where TOther : TCallbackArgument;

    public void Register(TKey key);

    public void Unregister(TKey key);
}
