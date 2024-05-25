using System.Collections.Generic;

namespace Asteroids.Model
{
    public class UpdateObjectsModel
    {
        private readonly List<IUpdate> updateObjects = new();

        public UpdateObjectsModel(IObjectSpawn[] modelSpawn, IObjectDestroy modelDestroy)
        {
            foreach (var creator in modelSpawn)
            {
                creator.OnObjectSpawn += OnObjectSpawn;
            }
            modelDestroy.OnObjectDestroy += OnObjectDestroy;
        }

        public void Add(IUpdate value)
        {
            updateObjects.Add(value);
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < updateObjects.Count; i++)
            {
                updateObjects[i].Update(deltaTime);
            }
        }

        private void OnObjectSpawn(object moveObject)
        {
            if (moveObject is IUpdate)
            {
                updateObjects.Add(moveObject as IUpdate);
            }
        }

        private void OnObjectDestroy(object value)
        {
            if (value is IUpdate)
            {
                updateObjects.Remove(value as IUpdate);
            }
        }
    }

}
