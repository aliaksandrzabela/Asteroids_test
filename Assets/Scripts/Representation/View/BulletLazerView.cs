using UnityEngine;

public class BulletLazerView : AbstractModelView
{
    private BulletLazer model;
    private Camera objCamera;

    void Update()
    {
        transform.position = model.Position;
    }

    public override void Init(Camera camera, object model)
    {
        objCamera = camera;
        model = model as BulletLazer;
        if(model == null)
        {
            Debug.LogError("Wrong model. For BulletLazer.");
        }
    }
}
