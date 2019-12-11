using Godot;

namespace SuperBrickBreaker.GameObjects
{
    public abstract class GamePowerUp<T> : Area2D  where T : Node, IPowerUp<T>
    {
        public abstract GameEnumerations.PowerUp Type { get; }
        public abstract string ResourcePath { get; }
        public abstract void Apply();

        public abstract void Stop();
        public virtual Godot.Resource GetPackedScene() {
            Godot.Resource scene = null;
            if(ResourcePath != null){
                scene = ResourceLoader.Load(ResourcePath);
            }            
            return scene;
        }

        public T GetInstance()
        {
            return this.GetPackedScene() as T;
        }
    }
}