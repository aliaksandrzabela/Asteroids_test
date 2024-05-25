using System;

namespace Asteroids.Model
{
    public interface IObjectDestroy
    {
        event Action<object> OnObjectDestroy;
    }
}
