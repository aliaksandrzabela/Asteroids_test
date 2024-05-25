using System;

namespace Asteroids.Model
{
    public interface IObjectSpawn
    {
        event Action<object> OnObjectSpawn;
    }
}

