using ConsoleGameEngine.Core.Entities;
using ConsoleGameEngine.Graphics.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

public interface IRenderSystem
{
    SpriteRenderer SpriteRenderer { get; }

    void SubscribeRenderer<TEntity>(Action<TEntity> renderHandler) where TEntity : GameEntity;

    void UnsubscribeRenderer<TEntity>() where TEntity : GameEntity;

    void SubscribeUpdateRenderer<TEntity>(Action<TEntity> renderHandler) where TEntity : GameEntity;

    void UnsubscribeUpdateRenderer<TEntity>() where TEntity : GameEntity;

    void SubscribeUnrenderer<TEntity>(Action<TEntity> renderHandler) where TEntity : GameEntity;

    void UnsubscribeUnrenderer<TEntity>() where TEntity : GameEntity;

    void Render<TEntity>(TEntity entity) where TEntity : GameEntity;

    void RenderUpdate<TEntity>(TEntity entity) where TEntity : GameEntity;

    void Unrender<TEntity>(TEntity entity) where TEntity : GameEntity;
}
