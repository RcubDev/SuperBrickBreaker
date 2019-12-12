using Godot;
using System;
using SuperBrickBreaker.GameObjects.Helpers;
using GameEnumerations;

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
            ResetGame();
        }

        if (gameOver && Input.IsActionJustPressed("ui_start_game"))
        {
            GameManagerHelper.GetGameManager(this).GameStarted = true;
            gameOver = false;
            EmitSignal(MainSignal.GameStarted.ToString(), new object[] {null});
        }
    }

    private void ResetGame(){
        GameManagerHelper.GetGameManager(this).GameStarted = false;
        var balls = GetTree().GetNodesInGroup(Groups.Balls.ToString());
        foreach(var ball in balls){
            var castBall = ball as Ball;
            castBall.Destroy();
        }
        GetTree().ReloadCurrentScene();

    }
}
