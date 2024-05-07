using UnityEngine;

public class MoveObjectView : AbstractModelView
{
    private IMoveObject model;
    private Camera objCamera;

    void Update()
    {
        transform.position = objCamera.ViewportToWorldPoint(GetViewportPosition());
        transform.rotation = Quaternion.Euler(0, 0, model.Angle);
    }

    public override void Init(Camera camera, object model)
    {
        objCamera = camera;
        this.model = model as IMoveObject;
        if (model == null)
        {
            Debug.LogError("Wrong model. For MoveObjectView.");
        }
    }

    private Vector3 GetViewportPosition()
    {
        return new Vector3(model.Position.x, model.Position.y, 1);
    }
}
