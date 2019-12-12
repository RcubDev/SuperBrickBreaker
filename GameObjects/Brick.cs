using Godot;
using System;
using GameEnumerations;
using Helpers;
using SuperBrickBreaker.GameObjects.Interfaces;
using SuperBrickBreaker.GameObjects;

public class Brick : RigidBody2D, IGroupEntity, IGameEntity
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
    [Export]
    int powerUp;
    [Export]
    bool randomizePowerUp = true;
    private BrickColor currentColor;
    private GamePowerUp containedPowerUp;

    public override void _Ready()
    {

        if (randomizePowerUp && RandomHelper.GetRandomNumber(1, 10) > 7)
        {
            SetBrickContainedPowerUp((PowerUp)RandomHelper.GetRandomNumber(1, 4));
        }
        else if (!randomizePowerUp && powerUp != 0)
        {
            SetBrickContainedPowerUp((PowerUp)powerUp);
        }

        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        if (startColor <= 0)
        {
            currentColor = colors[RandomHelper.GetRandomNumber(0, colors.Length)];
        }
        else
        {
            currentColor = (BrickColor)startColor;
        }
        AddNodeToGroup();
        sprite.Play(currentColor.ToString());
    }

    public void OnBrickBodyEntered(PhysicsBody2D body)
    {
        if (body is Ball)
        {
            if ((int)currentColor % 2 == 1)
            {
                currentColor++;
                sprite.Play(currentColor.ToString());
            }
            else
            {
                Destroy();
            }
        }
    }

    public void AddNodeToGroup()
    {
        AddToGroup(GameEnumerations.Groups.Bricks.ToString());
    }

    public void Destroy()
    {
        CallDeferred("DropPowerUp");        
        QueueFree();
    }

    private void DropPowerUp()
    {
        if (containedPowerUp != null)
        {
            containedPowerUp.Position = this.Position;
            GetParent().AddChild(containedPowerUp);
        }
    }

    public void RemoveNodeFromGroup()
    {
        RemoveFromGroup(GameEnumerations.Groups.Bricks.ToString());
    }

    private void SetBrickContainedPowerUp(PowerUp powerUpEnumerated)
    {
        PackedScene loadedPowerUp;
        switch (powerUpEnumerated)
        {
            case PowerUp.MultiBall:
                loadedPowerUp = ResourceLoader.Load("res://GameObjects/MultiBallPowerUp.tscn") as PackedScene;
                break;
            case PowerUp.DoubleDamage:
                loadedPowerUp = ResourceLoader.Load("res://GameObjects/MultiBallPowerUp.tscn") as PackedScene;
                 break;
            case PowerUp.FireBall:
                loadedPowerUp = ResourceLoader.Load("res://GameObjects/MultiBallPowerUp.tscn") as PackedScene;
                break;
            default:
                loadedPowerUp = ResourceLoader.Load("res://GameObjects/MultiBallPowerUp.tscn") as PackedScene;
                break;
        }
        containedPowerUp = loadedPowerUp.Instance() as GamePowerUp;
    }
}
