using UnityEngine;

public class LCircleCollider2D : LCollider2D
{
    [SerializeField]
    private LVector2 mCenter = LVector2.zero;

    [SerializeField]
    private LFloat mRadius = 1;

    private LVector2 mWorldCenter = LVector2.zero;

    public LVector2 center
    {
        get
        {
            return mCenter;
        }
        set
        {
            mCenter = value;
        }
    }

    public LFloat radius
    {
        get
        {
            return mRadius;
        }
        set
        {
            mRadius = value;
        }
    }

    internal override void PrepareForCollision()
    {
        base.PrepareForCollision();
        mWorldCenter = transform.TransformPoint(mCenter);
        LVector3 localScale = transform.localScale;
        LFloat radius = mRadius * (localScale.x > localScale.y ? localScale.x : localScale.y);
        LVector2 offset = new LVector2(radius, radius);
        mBounds.min = mWorldCenter - offset;
        mBounds.max = mWorldCenter + offset;
    }

    internal override LVector2 GetFurtherPointInDirection(LVector2 direction)
    {
        LVector2 fp = LVector2.zero;
        LVector3 localScale = transform.localScale;
        LFloat radius = mRadius * (localScale.x > localScale.y ? localScale.x : localScale.y);
        if (direction.x == 0)
        {
            fp.x = mWorldCenter.x;
            fp.y = (direction.y > 0 ? 1 : -1) * (mWorldCenter.y + radius);
        }
        else if (direction.y == 0)
        {
            fp.x = (direction.x > 0 ? 1 : -1) * (mWorldCenter.x + radius);
            fp.y = mWorldCenter.y;
        }
        else
        {
            direction = direction.normalized;
            fp.x = mWorldCenter.x + direction.x * radius;
            fp.y = mWorldCenter.y + direction.y * radius;
        }
        return fp;
    }
}
