using System;
using System.Collections.Generic;

namespace Asteroids.Model
{
    public class ObserverObjectDestroy : IObjectDestroy
    {
        public event Action<object> OnObjectDestroy;

        private List<IMoveObject> spawnAll;

        public ObserverObjectDestroy(List<IMoveObject> spawnAll)
        {
            this.spawnAll = spawnAll;
        }

        public void ModelDestroy(object value)
        {
            spawnAll.Remove(value as IMoveObject);
            OnObjectDestroy?.Invoke(value);
        }
    }
}
