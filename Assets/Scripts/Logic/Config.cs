
namespace Asteroids.Model
{
    public class Config
    {
        public const float LEVEL_TIME = 120f;
        public const int ASTEROIDS_ON_LEVEL = 25;

        public const float BULLET_SPEED = 0.93f;
        public const float BULLET_SIZE = 0.005f;
        public const float BULLET_LIFETIME = 1f;

        public const float ASTEROID_SIZE = 0.025f;
        public const float ASTEROID_SPEED = 0.1f;
        public const float ASTEROID_PART_SIZE = 0.007f;
        public const float ASTEROID_PART_SPEED = 0.15f;

        public const float SHIP_ACCELERATION = 0.35f;
        public const float SHIP_SIZE = 0.007f;
        public const float SHIP_ROTATION_SPEED = 180f;
        public const float BULLET_LAZER_LIFETIME = 0.5f;

        public const int LAZER_SHOOT_COUNT = 10;
        public const float LAZER_COOLDOWN = 4.5f;

        public const float UFO_SIZE = 0.007f;
        public const float UFO_ACCELERATION = 0.29f;
        public const float MAX_MOVE_SPEED = 0.5f;

        public const string TAG_ASTEROID = "asteroid";
        public const string TAG_UFO = "ufo";

        public const int SCORE_UFO = 20;
        public const int SCORE_ASTEROID = 10;
    }
}
