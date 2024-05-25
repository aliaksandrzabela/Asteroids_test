using UnityEngine;

namespace Asteroids.Model.View
{
    public abstract class AbstractModelView : MonoBehaviour
    {
        public abstract object Model { get; }
        public abstract void Init(Camera camera, object model);
    }
}
