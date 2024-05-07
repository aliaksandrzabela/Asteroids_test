using System.Collections.Generic;

public class ColisionCheck
{
    private readonly List<IMoveObject> moveObjects;

    public ColisionCheck(List<IMoveObject> moveObjects)
    {
        this.moveObjects = moveObjects;
    }

    public List<(object, object)> CheckCollisions()
    {
        List<(object, object)> result = new();

        for (int i = 0; i < moveObjects.Count; i++)
        {
            for (int j = 0; j < moveObjects.Count; j++)
            {
                if (i == j) continue;
                if ((moveObjects[i].Position - moveObjects[j].Position).magnitude < moveObjects[i].Size + moveObjects[j].Size)
                {
                    if (!Contains(result, (moveObjects[i], moveObjects[j])))
                    {
                        result.Add((moveObjects[i], moveObjects[j]));
                    }                   
                }
            }
        }
        return result;
    }

    private bool Contains(List<(object, object)> pairs, (object, object) pair)
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
