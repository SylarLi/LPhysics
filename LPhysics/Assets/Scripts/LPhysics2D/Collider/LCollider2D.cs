using System;
using System.Collections.Generic;
using UnityEngine;

public class LCollider2D : MonoBehaviour
{
    [SerializeField]
    protected Bounds mBounds;

    [SerializeField]
    private LFloat mRestitution;

    [SerializeField]
    private LFloat mFriction;

    private bool mHasMoved;
     
    public bool hasMoved
    {
        get
        {
            return mHasMoved;
        }
        set
        {
            mHasMoved = value;
        }
    }

    public Bounds bounds
    {
        get
        {
            return mBounds;
        }
    }

    public LFloat restitution
    {
        get
        {
            return mRestitution;
        }
        set
        {
            mRestitution = value;
        }
    }

    public LFloat friction
    {
        get
        {
            return mFriction;
        }
        set
        {
            mFriction = value;
        }
    }

    private void OnEnable()
    {
        LPhysics2D.inst.Update(transform);
    }

    private void OnDisable()
    {
        LPhysics2D.inst.Update(transform);
    }

    /// <summary>
    /// 在碰撞检测前调用的预处理
    /// </summary>
    internal virtual void PrepareForCollision()
    {

    }

    /// <summary>
    /// 获得某个方向上的最远点
    /// <para>For GJK</para>
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    internal virtual LVector2 GetFurtherPointInDirection(LVector2 direction)
    {
        throw new System.NotImplementedException();
    }
}
