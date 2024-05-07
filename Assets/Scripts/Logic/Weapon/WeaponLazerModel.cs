using System;
using UnityEngine;

public class WeaponLazerModel : WeaponModel
{
    public event Action<int> OnBulletCountChanged;
    public int Bullets { get; private set; }
    public int MaxBullets { get; private set; }
    public float RestoreLazerTime { get; private set; }

    private float cooldown;

    public WeaponLazerModel(int bullets, float cooldown)
    {
        Bullets = MaxBullets = bullets;
        this.cooldown = RestoreLazerTime = cooldown;
    }

    protected override Bullet CreateBullet(Vector2 position, Vector2 direction)
    {
        if(Bullets > 0)
        {
            Bullets--;
            OnBulletCountChanged?.Invoke(Bullets);
            return new BulletLazer(position, direction);
        }
        return null;
    }

    public void Update(float deltaTime)
    {        
        if (Bullets < MaxBullets)
        {
            RestoreLazerTime -= deltaTime;

            if (RestoreLazerTime <= 0)
            {
                RestoreLazerTime = cooldown;
                Bullets++;
                OnBulletCountChanged?.Invoke(Bullets);
            }
        }

    }
}
