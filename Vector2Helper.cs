using Godot;
using System;

public static class Vector2Helper
{
    public static void PrintVector2(Vector2 vector, string name)
    {
        GD.Print($"{name ?? string.Empty}x: ${vector.x}, y: {vector.y}");
    }
}
