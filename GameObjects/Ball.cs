using Godot;
using System;
public class Ball : RigidBody2D
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
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
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

    public override void _IntegrateForces(Physics2DDirectBodyState state) {
        if(isFollowingPaddle){
            var transform = state.GetTransform();
            transform.origin.x = (_paddle.Position + ballOffsetFromPaddle).x;
            transform.origin.y = (_paddle.Position + ballOffsetFromPaddle).y;
            state.SetTransform(transform);            
        }
    }


    public void StartGame() {
        ballMotion = Vector2.Up + startVector[rand.Next(0, 2)];
        ApplyImpulse(Vector2.Zero, ballMotion * startSpeed);
        isFollowingPaddle = false;
    }
}
