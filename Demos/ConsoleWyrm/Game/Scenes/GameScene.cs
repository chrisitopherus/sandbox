using ConsoleGameEngine.Core;
using ConsoleGameEngine.Graphics.Sprites;
using ConsoleGameEngine.Physics.Collisions;
using ConsoleWyrm.Game.Entities.Consumables;
using ConsoleWyrm.Game.Entities.Wyrms;
using ConsoleWyrm.Game.Graphics;
using ConsoleWyrm.Game.Ressources;
using GameStuff.Data;
using Helpers.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Scenes;

public class GameScene : Scene
{
    private List<Wyrm> wyrms = [];

    public override void Render()
    {
        foreach (GameEntity entity in this.gameEntities.Where(e => e.IsDirty))
        {
            if (entity is Wyrm)
            {
                Wyrm wyrm = (Wyrm)entity;
                this.UndrawChar(wyrm.PreviousPosition);
                this.DrawWyrmHead(wyrm);
                this.DrawFirstSegment(wyrm);
                this.UndrawLastSegment(wyrm);
            }
            else
            {
                this.UndrawChar(entity.PreviousPosition);
                this.DrawSprite(entity.Sprite, entity.Position);
            }

            entity.ClearDirty();
        }
    }
    protected override void Init()
    {
        Sprite wyrmSprite = RessourceRegistry.GetSprite(SpriteId.WyrmHead);
        ICollisionShape pointCollisionShape = RessourceRegistry.GetCollisionShape(CollisionShapeId.Point);
        ConsolePosition position = new ConsolePosition(10, 10);
        Wyrm wyrm = new(wyrmSprite, pointCollisionShape, position);
        this.wyrms.Add(wyrm);

        Sprite foodSprite = new(["F"], new ConsoleStyle(ConsoleColor.Red, ConsoleColor.Black));
        ConsolePosition foodPos = new ConsolePosition(20, 10);
        Food food = new(foodSprite, pointCollisionShape, foodPos);
        this.gameEntities.Add(wyrm);
        this.gameEntities.Add(food);
    }

    private void DrawWyrmHead(Wyrm wyrm)
    {
        this.DrawSprite(wyrm.Sprite, wyrm.Position);
    }

    private void DrawFirstSegment(Wyrm wyrm)
    {
        WyrmSegment? firstSegment = wyrm.Tail.First?.Value;
        if (firstSegment == null)
        {
            return;
        }

        this.DrawSprite(firstSegment.Sprite, firstSegment.Position);
    }

    private void UndrawLastSegment(Wyrm wyrm)
    {
        WyrmSegment? lastSegment = wyrm.Tail.Last?.Value;
        if (lastSegment == null)
        {
            return;
        }

        this.UndrawChar(lastSegment.PreviousPosition);
    }

    private void UndrawChar(ConsolePosition position)
    {
        Console.SetCursorPosition(position.X, position.Y);
        ConsoleTheme.DefaultStyle.Apply();
        Console.Write(" ");
    }

    private void DrawSprite(Sprite sprite, ConsolePosition position)
    {
        sprite.Style.Apply();
        Console.SetCursorPosition(position.X, position.Y);

        for (int i = 0; i < sprite.Lines.Length; i++)
        {
            string line = sprite.Lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                Console.Write(line[j]);
            }

            if (i != sprite.Lines.Length - 1)
            {
                Console.WriteLine();
                Console.SetCursorPosition(position.X, position.Y + (i + 1));
            }
        }
    }
}
