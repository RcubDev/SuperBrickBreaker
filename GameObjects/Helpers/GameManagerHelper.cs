using Godot;

namespace SuperBrickBreaker.GameObjects.Helpers
{
    public class GameManagerHelper
    {
        public static GameManager GetGameManager(Node node) {
            return (GameManager)node.GetNode("/root/GameManager");
        }    
    }
}