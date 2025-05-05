using ConsoleGameEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Collisions.Detector;

public class Collision
{
    public Collision(ICollidable a, ICollidable b)
    {
        this.A = a; 
        this.B = b;
    }

    public ICollidable A { get; }
    public ICollidable B { get; }
}
