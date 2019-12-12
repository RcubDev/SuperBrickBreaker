using Godot;
using SuperBrickBreaker.GameObjects.Interfaces;

namespace SuperBrickBreaker.GameObjects
{
    public abstract class GamePowerUp : Area2D, IPowerUp, IGameEntity
    {
        public abstract GameEnumerations.PowerUp Type { get; }

        public abstract void Apply();

        public abstract void Destroy();

        public abstract void Stop();
    }
}