using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Model
{
    public class ColisionCheck
    {
        private readonly List<IMoveObject> moveObjects;

        public ColisionCheck(List<IMoveObject> moveObjects)
        {
            this.moveObjects = moveObjects;
        }

        public IEnumerable<(object, object)> CheckCollisions()
        {
            List<(object, object)> result = new();

            for (int i = 0; i < moveObjects.Count; i++)
            {
                for (int j = 0; j < moveObjects.Count; j++)
                {
                    if (i == j) continue;
                    if ((moveObjects[i].Position - moveObjects[j].Position).magnitude < moveObjects[i].Size + moveObjects[j].Size)
                    {
                        if (!result.ContainsPair((moveObjects[i], moveObjects[j])))
                        {
                            result.Add((moveObjects[i], moveObjects[j]));
                        }
                    }
                }
            }
            return result;
        }
    }
}
