using System;
using System.Collections.Generic;

namespace Asteroids.Model
{
    public class CollisionObjectLogic
    {
        public event Action OnShipCollide;

        private readonly ColisionCheck colisionCheckModel;
        private readonly List<IMoveObject> moveObjects;
        private readonly ObjectsSpawner objectsSpawner;
        private readonly ObserverObjectDestroy observerObjectDestroy;

        public CollisionObjectLogic(ObjectsSpawner objectsSpawner, ObserverObjectDestroy observerObjectDestroy)
        {
            this.moveObjects = objectsSpawner.allMoveObjects;
            this.objectsSpawner = objectsSpawner;
            this.observerObjectDestroy = observerObjectDestroy;
            colisionCheckModel = new ColisionCheck(moveObjects);
        }

        public void ProcessCollisions()
        {
            var collisions = colisionCheckModel.CheckCollisions();
            foreach (var pair in collisions)
            {
                if (pair.Item1 is ShipModel)
                {
                    if (pair.Item2 is ShipUFOModel || pair.Item2 is AsteroidModel)
                    {
                        OnShipCollide?.Invoke();
                    }
                }

                if (pair.Item1 is Bullet || pair.Item2 is Bullet)
                {
                    object other = (pair.Item1 is Bullet) ? pair.Item2 : pair.Item1;
                    if (other is ShipModel || other is Bullet)
                    {
                        continue;
                    }

                    if (other is AsteroidModel)
                    {
                        var asteroid = other as AsteroidModel;
                        if (asteroid.Size >= Config.ASTEROID_SIZE)
                        {
                            objectsSpawner.CreateAsteroidParts(asteroid.Position);
                        }
                    }
                    observerObjectDestroy.ModelDestroy(pair.Item1);
                    observerObjectDestroy.ModelDestroy(pair.Item2);
                }
            }
        }
    }

}
