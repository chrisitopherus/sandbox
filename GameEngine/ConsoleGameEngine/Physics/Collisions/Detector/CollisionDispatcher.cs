using ConsoleGameEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Collisions.Detector;

public static class CollisionDispatcher
{
    private static readonly Dictionary<(Type, Type), MethodInfo?> cache = [];

    public static void Dispatch(ICollidable handler, ICollidable other)
    {
        Type handlerType = handler.GetType();
        Type otherType = other.GetType();
        (Type, Type) key = (handlerType, otherType);

        if (!cache.TryGetValue(key, out var method))
        {
            Type? iface = typeof(ICollidesWith<>).MakeGenericType(otherType);
            method = iface.IsAssignableFrom(handlerType) ? iface.GetMethod("OnCollision") : null;
            cache[key] = method;
        }

        method?.Invoke(handler, [other]);
    }
}
