using UnityEngine;

public interface IMoveObject
{
    float Size { get; }
    Vector2 Position { get; set; }
    Vector2 Velocity { get; set; }

    float Angle { get; set; }
}
