using System;
using System.Collections.Generic;

public class CollisionObjectLogic
{
    public event Action OnShipCollide;
    public event Action<object> OnObjectDestroy;

    private readonly ColisionCheck colisionCheckModel;
    private readonly List<IMoveObject> moveObjects;
    private readonly ObjectsSpawner objectsSpawner;

    public CollisionObjectLogic(ObjectsSpawner objectsSpawner)
    {
        this.moveObjects = objectsSpawner.AllSpawned;
        this.objectsSpawner = objectsSpawner;
        colisionCheckModel = new ColisionCheck(moveObjects);
    }

    public void ProcessCollisions()
    {
        var objectsPair = colisionCheckModel.CheckCollisions();
        foreach(var pair in objectsPair)
        {
            if(pair.Item1 is ShipModel)
            {
                if (pair.Item2 is ShipUFOModel || pair.Item2 is AsteroidModel)
                {
                    OnShipCollide?.Invoke();
                }
            }

            if(pair.Item1 is Bullet && !(pair.Item2 is ShipModel) && !(pair.Item2 is Bullet))
            {
                if(pair.Item2 is AsteroidModel)
                {
                    var asteroid = pair.Item2 as AsteroidModel;
                    if(asteroid.Size >= Config.ASTEROID_SIZE)
                    {
                        objectsSpawner.CreateAsteroidParts(asteroid.Position);
                    }
                }

                moveObjects.Remove(pair.Item1 as IMoveObject);
                moveObjects.Remove(pair.Item2 as IMoveObject);

                OnObjectDestroy?.Invoke(pair.Item1);
                OnObjectDestroy?.Invoke(pair.Item2);
            }
        }
    }
}
