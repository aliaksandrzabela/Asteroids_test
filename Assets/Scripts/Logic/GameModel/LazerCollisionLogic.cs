using System;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.Model.View;

namespace Asteroids.Model
{
    public class LazerCollisionLogic
    {
        public Action<BulletLazer> OnBulletCreate;
        public Action<BulletLazer> OnBulletDestroy;

        private readonly List<BulletLazer> bullets = new();
        private readonly Camera camera;
        private readonly ObserverObjectDestroy observerObjectDestroy;

        public LazerCollisionLogic(Camera camera, ObserverObjectDestroy observerObjectDestroy)
        {
            this.camera = camera;
            this.observerObjectDestroy = observerObjectDestroy;
            observerObjectDestroy.OnObjectDestroy += OnObjectDestroy;
        }

        public void Add(BulletLazer bullet)
        {
            bullets.Add(bullet);
            OnBulletCreate?.Invoke(bullet);
        }

        private List<object> GetCollisions()
        {
            List<object> result = new();

            for (int i = 0; i < bullets.Count; i++)
            {
                RaycastHit2D[] rays = Physics2D.RaycastAll(camera.ViewportToWorldPoint(bullets[i].Position), bullets[i].Direction);
                for (int j = 0; j < rays.Length; j++)
                {
                    if (rays[j].collider != null)
                    {
                        if (rays[j].collider.CompareTag(Config.TAG_ASTEROID) || rays[j].collider.CompareTag(Config.TAG_UFO))
                        {
                            var component = rays[j].collider.GetComponent<AbstractModelView>();
                            if (component != null && !result.Contains(component.Model))
                            {
                                result.Add(component.Model);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public void ProcessCollisions()
        {
            var collisions = GetCollisions();
            foreach (var model in collisions)
            {
                observerObjectDestroy.ModelDestroy(model);
            }
        }

        private void OnObjectDestroy(object value)
        {
            if (value is BulletLazer)
            {
                bullets.Remove(value as BulletLazer);
            }
        }
    }
}
