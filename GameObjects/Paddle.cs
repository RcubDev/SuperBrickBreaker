using Godot;
using SuperBrickBreaker.GameObjects;
using System;

public class Paddle : RigidBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    float moveSpeed = 100;
    float deceleration = 1000;

    Vector2 motion = Vector2.Zero;
    Vector2 lastInputDir = Vector2.Zero;
    Area2D powerUpArea;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        powerUpArea = GetNode<Area2D>("Area2D");
        powerUpArea.Connect("area_entered", this, nameof(PowerUpAreaEntered));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        GD.Print(GetViewportRect().Size.x);
        GD.Print(GetViewportRect().Size.y);
        GD.Print(this.Position.x);
        var inputDirection = GetInputDir();
        if(!true){
            if(inputDirection.x == 0){
                motion = motion.LinearInterpolate(Vector2.Zero, .1f);       
            }
            else{
                lastInputDir = inputDirection;            
                motion = (inputDirection.Normalized() * moveSpeed);
            }            
            Position = motion * delta;
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
    
    public void PowerUpAreaEntered(Area2D node){
        if(node is GamePowerUp){
            var castedNode = (GamePowerUp)node;
            castedNode.CallDeferred("Apply");
            castedNode.Destroy();
        }
    }
}
