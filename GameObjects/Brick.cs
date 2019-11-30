using Godot;
using System;
using GameEnumerations;
using Helpers;

public class Brick : RigidBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    AnimatedSprite sprite;
    BrickColor[] colors = { BrickColor.blue, BrickColor.brown, BrickColor.darkgreen, BrickColor.green, BrickColor.grey,
                            BrickColor.lightblue, BrickColor.orange, BrickColor.yellow, BrickColor.red, BrickColor.purple };

    [Export]
    int startColor;
    private BrickColor currentColor;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        if(startColor <= 0){
            currentColor = colors[RandomHelper.GetRandomNumber(0, colors.Length)];
        }
        else{
            currentColor = (BrickColor)startColor;
        }

        sprite.Play(currentColor.ToString());
        AddToGroup(GameEnumerations.Groups.bricks.ToString());
    }

    public void OnBrickBodyEntered(PhysicsBody2D body)
    {
        if((int)currentColor % 2 == 1){
            currentColor++;
            sprite.Play(currentColor.ToString());
        }
        else {
            QueueFree();
        }        
    }
}
