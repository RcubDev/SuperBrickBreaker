using Godot;
using System;

public class Paddle : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    float moveSpeed = 100;
    float deceleration = 1000;

    Vector2 motion = Vector2.Zero;
    Vector2 lastInputDir = Vector2.Zero;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {

        var inputDirection = GetInputDir();
        if(!true){
            if(inputDirection.x == 0){
                motion = motion.LinearInterpolate(Vector2.Zero, .1f);       
            }
            else{
                lastInputDir = inputDirection;            
                motion = (inputDirection.Normalized() * moveSpeed);
            }
            MoveAndCollide(motion * delta);

        }
        else{
            Position = new Vector2(GetGlobalMousePosition().x, Position.y);

        }



    }

    private Vector2 GetInputDir(){
        Vector2 inputDirection = new Vector2();

        if(Input.IsActionPressed("ui_right")){
            inputDirection.x++;
        }

        if(Input.IsActionPressed("ui_left")){
            inputDirection.x--;
        }
        return inputDirection;
    }
}
