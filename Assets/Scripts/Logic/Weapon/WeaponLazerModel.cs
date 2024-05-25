using System;
using UnityEngine;

namespace Asteroids.Model
{
    public class WeaponLazerModel : WeaponModel, IObjectSpawn, IUpdate
    {
        public event Action<int> OnBulletCountChanged;
        public event Action<object> OnObjectSpawn;

        public int Bullets { get; private set; }
        public int MaxBullets { get; private set; }
        public float RestoreLazerTime { get; private set; }

        private readonly float cooldown;
        private readonly LazerCollisionLogic lazerCollisionLogic;
        private readonly ObserverObjectDestroy observerObjectDestroy;

        public WeaponLazerModel(int bullets, float cooldown, LazerCollisionLogic lazerCollisionLogic, ObserverObjectDestroy observerObjectDestroy)
        {
            Bullets = MaxBullets = bullets;
            this.cooldown = RestoreLazerTime = cooldown;
            this.lazerCollisionLogic = lazerCollisionLogic;
            this.observerObjectDestroy = observerObjectDestroy;
        }

        protected override Bullet CreateBullet(Vector2 position, Vector2 direction)
        {
            if (Bullets > 0)
            {
                Bullets--;
                OnBulletCountChanged?.Invoke(Bullets);
                BulletLazer result = new BulletLazer(position, direction, observerObjectDestroy, Config.BULLET_LAZER_LIFETIME);
                lazerCollisionLogic.Add(result);
                OnObjectSpawn?.Invoke(result);
                return result;
            }
            return null;
        }

        public void Update(float deltaTime)
        {
            if (Bullets < MaxBullets)
            {
                RestoreLazerTime -= deltaTime;

                if (RestoreLazerTime <= 0)
                {
                    RestoreLazerTime = cooldown;
                    Bullets++;
                    OnBulletCountChanged?.Invoke(Bullets);
                }
            }

        }
    }

}
