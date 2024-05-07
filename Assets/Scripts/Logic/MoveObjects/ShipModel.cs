using System;
using UnityEngine;
public class ShipModel : IMoveObject
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Angle { get; set; }
    public float Size => size;

    private readonly float rotateSpeed;
    private readonly float acceleration;
    private readonly float size;

    private WeaponModel lazer;
    private WeaponModel gun;

    public ShipModel(Vector2 postion, float size, float rotateSpeed, float acceleration)
    {
        Position = postion;
        this.rotateSpeed = rotateSpeed;
        this.acceleration = acceleration;
        this.size = size;
    }

    public void InitWeapons(WeaponModel gun, WeaponModel lazer)
    {
        this.gun = gun;
        this.lazer = lazer;
    }
    public void Rotate(float direction, float deltaTime)
    {
         Angle = Mathf.Repeat(Angle + direction * rotateSpeed * deltaTime, 360); ;
    }

    public void TurnLeft(float deltaTime) => Rotate(-1, deltaTime);

    public void TurnRight(float deltaTime) => Rotate(1, deltaTime);

    public void Accelerate(float deltaTime)
    {
        Velocity = new Vector2(Velocity.x - Mathf.Sin(Angle * Mathf.Deg2Rad) * acceleration * deltaTime,
            Velocity.y + Mathf.Cos(Angle * Mathf.Deg2Rad) * acceleration * deltaTime);
    }

    public void ShootGun() => Shoot(gun);

    public void ShootLaser() => Shoot(lazer);

    private void Shoot(WeaponModel weapon)
    {
        weapon?.Shoot(Position, new Vector2(-Mathf.Sin(Angle * Mathf.Deg2Rad), Mathf.Cos(Angle * Mathf.Deg2Rad)));
    }
}
