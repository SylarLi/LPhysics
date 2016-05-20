using UnityEngine;

public class SimplexPoint2D
{
    private LVector2 mp;

    private LVector2 ma;

    private LVector2 mb;

    public SimplexPoint2D(LVector2 a, LVector2 b)
    {
        ma = a;
        mb = b;
        mp = ma - mb;
    }

    public LVector2 p
    {
        get
        {
            return mp;
        }
    }

    public LVector2 a
    {
        get
        {
            return ma;
        }
    }

    public LVector2 b
    {
        get
        {
            return mb;
        }
    }

    public bool Equals(SimplexPoint2D sp)
    {
        return ma == sp.a && mb == sp.b;
    }
}
