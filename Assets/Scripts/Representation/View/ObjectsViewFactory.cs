using Asteroids.Model;
using Asteroids.Model.View;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.View
{
    public class ObjectsViewFactory : MonoBehaviour
    {
        [SerializeField] private AbstractModelView asteroid;
        [SerializeField] private AbstractModelView partOfAsteroid;
        [SerializeField] private AbstractModelView ufo;

        [SerializeField] private AbstractModelView bullet;
        [SerializeField] private AbstractModelView bulletLazer;
        [SerializeField] private Camera viewsCamera;

        private Dictionary<object, AbstractModelView> modelView = new();

        //TODO: Create ObjectPool
        private AbstractModelView GetPrefab(object model)
        {
            if (model is AsteroidModel)
            {
                if ((model as AsteroidModel).Size >= Config.ASTEROID_SIZE)
                {
                    return asteroid;
                }
                return partOfAsteroid;
            }
            else if (model is ShipUFOModel)
            {
                return ufo;
            }
            else if (model is BulletGun)
            {
                return bullet;
            }
            else if (model is BulletLazer)
            {
                return bulletLazer;
            }

            throw new InvalidOperationException();
        }

        public void Create(object model)
        {
            var newView = Instantiate(GetPrefab(model));
            newView.Init(viewsCamera, model);
            modelView.Add(model, newView);
        }

        public void Remove(object model)
        {
            modelView.Remove(model, out var value);
            Destroy(value.gameObject);
        }
    }
}