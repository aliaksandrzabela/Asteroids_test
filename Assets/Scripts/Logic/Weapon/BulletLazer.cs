using UnityEngine;

namespace Asteroids.Model
{
    public class BulletLazer : Bullet
    {
        public readonly Vector2 Direction;

        public BulletLazer(Vector2 position, Vector2 direction, ObserverObjectDestroy observerObjectDestroy, float livetime) : base(position, observerObjectDestroy, livetime)
        {
            Direction = direction;
        }
    }
}
