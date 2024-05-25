using System.Collections.Generic;

namespace Asteroids.Model
{
    public static class CheckContainsExtinsion
    {
        public static bool ContainsPair(this List<(object, object)> pairs, (object, object) pair)
        {
            foreach (var (left, right) in pairs)
            {
                if (left == pair.Item1 && right == pair.Item2)
                    return true;

                if (left == pair.Item2 && right == pair.Item1)
                    return true;
            }
            return false;
        }
    }
}
