using UnityEngine;

public class BulletLazer : Bullet
{
    public readonly Vector2 Direction;

    public BulletLazer(Vector2 position, Vector2 direction) : base(position)
    {
        Direction = direction;
    }
}
