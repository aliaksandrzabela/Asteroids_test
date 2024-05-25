using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Model
{
    public class BordersCheckModel
    {
        private readonly List<IMoveObject> moveObjects;

        public BordersCheckModel(List<IMoveObject> moveObjects)
        {
            this.moveObjects = moveObjects;
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < moveObjects.Count; i++)
            {
                moveObjects[i].Position += moveObjects[i].Velocity * deltaTime;

                //check viewport bounds. Viewport: from [0,0] to [1,1]
                if (moveObjects[i].Position.x < 0f)
                {
                    moveObjects[i].Position = new Vector2(1f, moveObjects[i].Position.y);
                }

                if (moveObjects[i].Position.x > 1f)
                {
                    moveObjects[i].Position = new Vector2(0f, moveObjects[i].Position.y);
                }

                if (moveObjects[i].Position.y > 1f)
                {
                    moveObjects[i].Position = new Vector2(moveObjects[i].Position.x, 0f);
                }

                if (moveObjects[i].Position.y < 0f)
                {
                    moveObjects[i].Position = new Vector2(moveObjects[i].Position.x, 1f);
                }
            }
        }
    }
}
