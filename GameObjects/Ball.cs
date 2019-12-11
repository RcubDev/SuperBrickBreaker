using Godot;
using SuperBrickBreaker.GameObjects.Helpers;
using SuperBrickBreaker.GameObjects.Interfaces;
using System;
public class Ball : RigidBody2D, IGameEntity, IGroupEntity
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    Vector2 ballMotion = Vector2.Zero;
    Random rand = new Random();
    private Paddle _paddle;
    bool isFollowingPaddle = true;
    private Vector2[] startVector = { Vector2.Right, Vector2.Left };

    private Vector2 ballOffsetFromPaddle = new Vector2(0, -30);
    [Export]
    float startSpeed = 300;
    [Export]
    float maxSpeed = 1000;
    [Export]
    float minSpeed = 500;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (!GameManagerHelper.GetGameManager(this).GameStarted)
        {
            GameBallGetReady();
        }
        else
        {
            StartGame(200);
        }
        AddToGroup(GameEnumerations.Groups.Balls.ToString());
    }

    private void GameBallGetReady()
    {
        var paddle = GetParent().FindNode("Paddle") as Paddle;
        var mainScene = GetParent();
        mainScene.Connect(MainSignal.GameStarted.ToString(), this, nameof(StartGame));

        if (paddle != null)
        {
            _paddle = paddle;
            Position = _paddle.Position + ballOffsetFromPaddle;
        }
    }

    public override void _PhysicsProcess(float delta)
    {

    }

    public void OnVisibilityExited() {
        GD.Print("Visibility Exited");
        this.Destroy();
    }

    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {
        if (Mathf.Abs(GetLinearVelocity().x) > maxSpeed || Mathf.Abs(GetLinearVelocity().y) > maxSpeed)
        {
            var newSpeed = GetLinearVelocity().Normalized() * maxSpeed;
            SetLinearVelocity(newSpeed);
        }

        if (Mathf.Abs(GetLinearVelocity().x) < minSpeed || Mathf.Abs(GetLinearVelocity().y) < minSpeed)
        {
            var newSpeed = GetLinearVelocity().Normalized() * minSpeed;
            SetLinearVelocity(newSpeed);
        }
        if (isFollowingPaddle)
        {
            var transform = state.GetTransform();
            transform.origin.x = (_paddle.Position + ballOffsetFromPaddle).x;
            transform.origin.y = (_paddle.Position + ballOffsetFromPaddle).y;
            state.SetTransform(transform);
        }
    }


    public void StartGame(float? newStartSpeed = null)
    {
        if(newStartSpeed != null){
            startSpeed = (float)newStartSpeed;
        }
        ballMotion = Vector2.Up;
        ApplyImpulse(Vector2.Zero, ballMotion * startSpeed);
        isFollowingPaddle = false;
    }

    public void Destroy()
    {
        QueueFree();
    }

    public void AddNodeToGroup()
    {
        AddToGroup(GameEnumerations.Groups.Balls.ToString());
    }

    public void RemoveNodeFromGroup()
    {
        RemoveFromGroup(GameEnumerations.Groups.Balls.ToString());        
    }
}
