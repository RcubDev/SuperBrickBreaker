using Godot;
using System;

public class Paddle : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    float moveSpeed = 100;
    Vector2 targetPosition;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
       targetPosition = Position;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        Vector2 inputDirection = new Vector2();

        if(Input.IsActionPressed("ui_right")){
            inputDirection.x++;
        }

        if(Input.IsActionPressed("ui_left")){
            inputDirection.x--;
        }

        if(inputDirection.x == 0){
            
        }   
        Vector2 velocity = (inputDirection.Normalized() * moveSpeed * delta);        
        MoveAndCollide(velocity);
    }
}
