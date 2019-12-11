public interface IPowerUp<out T> {
    GameEnumerations.PowerUp Type {get;}
    string ResourcePath {get;}
    void Apply();
    void Stop();
    Godot.Resource GetPackedScene();
    T GetInstance();
}