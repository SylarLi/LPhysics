using UnityEngine;

internal class LPhysicsObject2D
{
    public Transform transform;

    public LCollider2D collider;

    public LRigidBody2D rigidBody;

    public LPhysicsObject2D(Transform trans)
    {
        transform = trans;
    }

    public LFloat mass
    {
        get
        {
            if (rigidBody != null)
            {
                return rigidBody.isFixed ? LFloat.PositiveInfinity : rigidBody.mass;
            }
            else
            {
                return LFloat.PositiveInfinity;
            }
        }
    }

    public LFloat massInverse
    {
        get
        {
            if (rigidBody != null)
            {
                return rigidBody.isFixed ? 0 : rigidBody.massInverse;
            }
            else
            {
                return 0;
            }
        }
    }

    public LFloat inertia
    {
        get
        {
            if (rigidBody != null)
            {
                return rigidBody.isFixed ? 0 : rigidBody.inertia;
            }
            else
            {
                return 0;
            }
        }
    }

    public LFloat inertiaInverse
    {
        get
        {
            if (rigidBody != null)
            {
                return rigidBody.isFixed ? LFloat.PositiveInfinity : rigidBody.inertiaInverse;
            }
            else
            {
                return LFloat.PositiveInfinity;
            }
        }
    }

    /// <summary>
    /// 物理引擎作用时改变速度的入口(会受到isFixed的影响)
    /// </summary>
    public LVector2 linearVelocity
    {
        get
        {
            return rigidBody != null ? rigidBody.linearVelocity : LVector2.zero;
        }
        set
        {
            if (rigidBody != null && !rigidBody.isFixed)
            {
                rigidBody.linearVelocity = value;
            }
        }
    }

    /// <summary>
    /// 物理引擎作用时改变速度的入口(会受到isFixed的影响)
    /// </summary>
    public LFloat angularVelocity
    {
        get
        {
            return rigidBody != null ? rigidBody.angularVelocity : 0;
        }
        set
        {
            if (rigidBody != null && !rigidBody.isFixed)
            {
                rigidBody.angularVelocity = value;
            }
        }
    }

    public void Refresh()
    {
        collider = transform.GetComponent<LCollider2D>();
        rigidBody = transform.GetComponent<LRigidBody2D>();
    }

    public LVector2 GetWorldCenterOfMass()
    {
        return rigidBody != null ? rigidBody.GetWorldCenterOfMass() : (LVector2)LParser.Parse(transform.position);
    }
}
