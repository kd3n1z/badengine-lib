using SFML.System;

namespace Badengine;

public class Vector2 {
    public float X;
    public float Y;

    public Vector2() { }

    public Vector2(float x, float y) {
        X = x;
        Y = y;
    }

    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector2 operator *(Vector2 a, float b) => new(a.X * b, a.Y * b);
    public static Vector2 operator /(Vector2 a, float b) => new(a.X / b, a.Y / b);
    
    public static Vector2 Zero => new(0, 0);
    public static Vector2 One => new(1, 1);

    public static implicit operator Vector2f(Vector2 vector2) => new Vector2f(vector2.X, vector2.Y);
    public static implicit operator Vector2i(Vector2 vector2) => new Vector2i((int)vector2.X, (int)vector2.Y);
}