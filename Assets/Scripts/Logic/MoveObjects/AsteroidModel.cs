using UnityEngine;

namespace Asteroids.Model
{
    public class AsteroidModel : IMoveObject
    {
        public float Size => size;

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public float Angle { get; set; }

        private readonly float size;

        public AsteroidModel(Vector2 position, Vector2 velocity, float size)
        {
            Position = position;
            Velocity = velocity;

            this.size = size;
        }
    }
}
