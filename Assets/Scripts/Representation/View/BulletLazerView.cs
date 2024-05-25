using UnityEngine;
using Asteroids.Model;
using Asteroids.Model.View;

namespace Asteroids.View
{
    public class BulletLazerView : AbstractModelView
    {
        public override object Model => model;

        private BulletLazer model;
        private Camera objCamera;

        void Update()
        {
            transform.position = objCamera.ViewportToWorldPoint(GetViewportPosition());
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Angle(model.Direction)));
        }

        public override void Init(Camera camera, object model)
        {
            this.model = model as BulletLazer;
            objCamera = camera;
            if (model == null)
            {
                Debug.LogError("Wrong model. For BulletLazer.");
            }
        }

        private Vector3 GetViewportPosition()
        {
            return new Vector3(model.Position.x, model.Position.y, 1);
        }

        private float Angle(Vector2 direction)
        {
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
    }

}
