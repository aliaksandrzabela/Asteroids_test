using UnityEngine;

namespace Asteroids.Model
{
    public class ShipUFOModel : IMoveObject, IUpdate
    {
        public float Size => size;
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Angle { get; set; }

        private readonly float acceleration;
        private readonly float size;
        private readonly IMoveObject target;
        private readonly float maxMoveSpeed;

        public ShipUFOModel(Vector2 position, IMoveObject target, float size, float acceleration, float maxMoveSpeed)
        {
            Position = position;
            this.target = target;
            this.size = size;
            this.acceleration = acceleration;
            this.maxMoveSpeed = maxMoveSpeed;
        }

        public void Update(float deltaTime)
        {
            Velocity = Vector2.ClampMagnitude(Velocity + (target.Position - Position).normalized * acceleration * deltaTime, maxMoveSpeed);
        }
    }
}
