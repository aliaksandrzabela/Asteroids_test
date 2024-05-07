using UnityEngine;

public class ShipUFOModel : IMoveObject
{
    public float Size => size;
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Angle { get; set; }

    private readonly float acceleration;
    private readonly float size;
    private readonly IMoveObject target;    

    public  ShipUFOModel(Vector2 position, IMoveObject target, float size, float acceleration)
    {
        Position = position;
        this.target = target;
        this.size = size;
        this.acceleration = acceleration;
    }

    public void Update(float deltaTime)
    {
        Velocity = (target.Position - Position).normalized * acceleration * deltaTime;
    }
}
