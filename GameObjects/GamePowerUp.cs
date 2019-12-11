using Godot;

namespace SuperBrickBreaker.GameObjects
{
    public abstract class GamePowerUp : Area2D, IPowerUp
    {
        public abstract GameEnumerations.PowerUp Type { get; }

        public abstract void Apply();

        public abstract void Stop();
    }
}