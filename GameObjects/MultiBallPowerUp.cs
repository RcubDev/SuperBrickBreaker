using GameEnumerations;
using Godot;
using SuperBrickBreaker.GameObjects;
using SuperBrickBreaker.GameObjects.Interfaces;

public class MultiBallPowerUp : GamePowerUp, IGroupEntity, IGameEntity
{
    [Export]
    public float moveSpeed = 100f;    
    public override PowerUp Type { get; }
    public MultiBallPowerUp() {
        Type = PowerUp.MultiBall;
    }
    public override void Apply()
    {
        Godot.Collections.Array balls = GetTree().GetNodesInGroup(GameEnumerations.Groups.Balls.ToString());
        foreach(var ball in balls){
            //Get Location and spawn 2 other balls.
            var castBall = (Ball)ball;
            for(int i = 0; i < 2; i++) {
                var newBall = ResourceLoader.Load("res://GameObjects/Ball.tscn") as PackedScene;
                Ball instance = newBall.Instance() as Ball;
                instance.Position = new Vector2(castBall.Position.x + i * 100, castBall.Position.y);
                castBall.GetParent().AddChild(instance);
            }
        }
    }
    public override void Stop()
    {
        var balls = GetTree().GetNodesInGroup(GameEnumerations.Groups.Balls.ToString());
        foreach(var ball in balls){
            var castBall = (Ball)ball;
            castBall.Destroy();        
        }
    }

    public override void _Ready()
    {
        AddNodeToGroup();
    }
    
    public override void _PhysicsProcess(float delta)
    {
        Vector2 moveVector = Vector2.Down;
        Position += moveVector * delta * moveSpeed;
    }

    public void AddNodeToGroup()
    {
        AddToGroup(GameEnumerations.Groups.PowerUps.ToString());
    }

    public void RemoveNodeFromGroup()
    {
        RemoveFromGroup(GameEnumerations.Groups.PowerUps.ToString());
    }

    public void OnMultiBallPowerUpBodyEntered(PhysicsBody2D body){
        if(body is Paddle){
            Destroy();
        }
    }

    public void Destroy()
    {
        CallDeferred("Apply");
        QueueFree();
    }
}
