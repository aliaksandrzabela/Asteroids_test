using UnityEngine;

namespace Asteroids.Model
{
    public class BulletGun : Bullet, IMoveObject
    {
        public float Size => size;
        public Vector2 Velocity { get; set; }

        public float Angle { get; set; }

        private readonly float size;

        public BulletGun(Vector2 position, Vector2 direction, float speed, float size, float livetime, ObserverObjectDestroy observerObjectDestroy) : base(position, observerObjectDestroy, livetime)
        {
            Velocity = direction * speed;
            this.size = size;
        }
    }
}
