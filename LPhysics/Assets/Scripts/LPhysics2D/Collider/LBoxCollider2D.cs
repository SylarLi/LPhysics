using UnityEngine;

public class LBoxCollider2D : LCollider2D 
{
    [SerializeField]
    private LVector2 mCenter = LVector2.zero;

    [SerializeField]
    private LVector2 mSize = LVector2.one;

    private LVector2[] mLocalVertics = new LVector2[4];

    private LVector2[] mVertics = new LVector2[4];

    public LVector2 center
    {
        get
        {
            return mCenter;
        }
        set
        {
            mCenter = value;
            CalculateLocalVertics();
        }
    }

    public LVector2 size
    {
        get
        {
            return mSize;
        }
        set
        {
            mSize = value;
            CalculateLocalVertics();
        }
    }

    private void CalculateLocalVertics()
    {
        LVector2 size2 = mSize / 2;
        mLocalVertics[0] = mCenter - size2;
        mLocalVertics[1] = mCenter + new LVector2(size2.x, -size2.y);
        mLocalVertics[2] = mCenter + size2;
        mLocalVertics[3] = mCenter + new LVector2(-size2.x, size2.y);
    }

    internal override void PrepareForCollision()
    {
        base.PrepareForCollision();
        CalculateLocalVertics();
        for (int i = mLocalVertics.Length - 1; i >= 0; i--)
        {
            mVertics[i] = transform.TransformPoint(mLocalVertics[i]);
        }
        mBounds = LGeometryUtil2D.CalcBounds(mVertics);
    }

    internal override LVector2 GetFurtherPointInDirection(LVector2 direction)
    {
        LVector2 fp = mVertics[0];
        LFloat fpdot = LVector2.Dot(fp, direction);
        for (int i = mVertics.Length - 1; i >= 1; i--)
        {
            LFloat vdot = LVector2.Dot(mVertics[i], direction);
            if (vdot > fpdot)
            {
                fp = mVertics[i];
                fpdot = vdot;
            }
        }
        return fp;
    }
}
