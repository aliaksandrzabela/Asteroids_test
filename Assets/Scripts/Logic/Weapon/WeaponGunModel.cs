using UnityEngine;

namespace Asteroids.Model
{
    public class WeaponGunModel : WeaponModel
    {
        private readonly ObjectsSpawner objectsSpawner;
        private readonly ObserverObjectDestroy observerObjectDestroy;

        public WeaponGunModel(ObjectsSpawner objectsSpawner, ObserverObjectDestroy observerObjectDestroy)
        {
            this.objectsSpawner = objectsSpawner;
            this.observerObjectDestroy = observerObjectDestroy;
        }

        protected override Bullet CreateBullet(Vector2 position, Vector2 direction)
        {
            var bullet = new BulletGun(position, direction, Config.BULLET_SPEED, Config.BULLET_SIZE, Config.BULLET_LIFETIME, observerObjectDestroy);
            objectsSpawner.AddMoveObject(bullet);
            return bullet;
        }
    }
}

