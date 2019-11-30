using Godot;
using System;

public class Main : Node
{
    [Signal]
    public delegate void GameStarted();
    Paddle paddle;
    Ball ball;

    bool gameOver = true;
    public override void _Ready()
    {
        paddle = GetNode("Paddle") as Paddle;
        ball = GetNode("Ball") as Ball;
    }
    public override void _Process(float delta) {
        if(Input.IsActionJustPressed("ui_restart")) {
            GetTree().ReloadCurrentScene();
        }

        if (gameOver && Input.IsActionJustPressed("ui_start_game"))
        {
            gameOver = false;
            EmitSignal(MainSignal.GameStarted.ToString());
        }
    }
}
