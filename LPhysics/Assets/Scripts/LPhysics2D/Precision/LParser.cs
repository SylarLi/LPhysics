using UnityEngine;

public sealed class LParser
{
    public static LVector2 Parse(Vector2 v)
    {
        return new LVector2(v.x, v.y);
    }

    public static Vector2 Parse(LVector2 v)
    {
        return new Vector2((float)v.x.ToDouble(), (float)v.y.ToDouble());
    }

    public static LVector3 Parse(Vector3 v)
    {
        return new LVector3(v.x, v.y, v.z);
    }

    public static Vector3 Parse(LVector3 v)
    {
        return new Vector3((float)v.x.ToDouble(), (float)v.y.ToDouble(), (float)v.z.ToDouble());
    }
}
