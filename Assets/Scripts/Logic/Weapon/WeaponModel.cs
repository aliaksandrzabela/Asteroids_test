using System;
using UnityEngine;

public abstract class WeaponModel
{
    public virtual void Shoot(Vector2 position, Vector2 direction)
    {
        CreateBullet(position, direction);        
    }

    protected abstract Bullet CreateBullet(Vector2 position, Vector2 direction);
}
