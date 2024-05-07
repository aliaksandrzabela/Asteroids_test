using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsViewFactory : MonoBehaviour
{
    [SerializeField] private AbstractModelView asteroid;
    [SerializeField] private AbstractModelView partOfAsteroid;
    [SerializeField] private AbstractModelView ufo;

    [SerializeField] private AbstractModelView bullet;
    [SerializeField] private AbstractModelView bulletLazer;
    [SerializeField] private Camera viewsCamera;

    private Dictionary<object, AbstractModelView> modelView = new();

    //TODO: Create ObjectPool
    private AbstractModelView GetPrefab(object model)
    {
        if(model is AsteroidModel)
        {
            if((model as AsteroidModel).Size >= Config.ASTEROID_SIZE)
            {
                return asteroid;
            }
            return partOfAsteroid;
        }
        else if(model is ShipUFOModel)
        {
            return ufo;
        }
        else if(model is BulletGun)
        {
            return bullet;
        }
        else if(model is BulletLazer)
        {
            return bulletLazer;
        }

        throw new InvalidOperationException();
    }

    public void Create(object model)
    {
        Debug.Log("Create.");
        if(model is Bullet)
        {
            Debug.Log("create bullet view");
        }
        var newView = Instantiate(GetPrefab(model));
        newView.Init(viewsCamera, model);
        modelView.Add(model, newView);
    }

    public void Remove(object model)
    {
        modelView.Remove(model, out var value);
        if(value == null)
        {
            Debug.Log("value null");
        }
        Destroy(value.gameObject);
    }
}
