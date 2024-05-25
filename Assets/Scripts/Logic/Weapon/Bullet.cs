using UnityEngine;

namespace Asteroids.Model
{
    public abstract class Bullet : IUpdate
    {
        public Vector2 Position { get; set; }

        private readonly ObserverObjectDestroy observerObjectDestroy;

        private bool isActive = true;
        private float livetime;

        public Bullet(Vector2 position, ObserverObjectDestroy observerObjectDestroy, float livetime)
        {
            this.observerObjectDestroy = observerObjectDestroy;
            this.livetime = livetime;
            Position = position;
        }

        public void Update(float deltaTime)
        {
            livetime -= deltaTime;
            if (livetime <= 0 && isActive)
            {
                isActive = false;
                observerObjectDestroy.ModelDestroy(this);
            }
        }
    }

}
