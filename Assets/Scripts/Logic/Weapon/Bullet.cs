using UnityEngine;

public abstract class Bullet
{
    public Vector2 Position { get; set; }

    public Bullet(Vector2 position)
    {
        Position = position;
    }
}
