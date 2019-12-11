
public interface IPowerUp {
    GameEnumerations.PowerUp Type {get;}
    void Apply();
    void Stop();
}