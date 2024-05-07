using UnityEngine;

public class WeaponGunModel : WeaponModel
{
    private readonly ObjectsSpawner objectsSpawner;

    public WeaponGunModel(ObjectsSpawner objectsSpawner)
    {
        this.objectsSpawner = objectsSpawner;
    }
    
    protected override Bullet CreateBullet(Vector2 position, Vector2 direction)
    {
        var bullet = new BulletGun(position, direction, Config.BULLET_SPEED, Config.BULLET_SIZE);
        objectsSpawner.AddMoveObject(bullet);
        return bullet;
    }
}