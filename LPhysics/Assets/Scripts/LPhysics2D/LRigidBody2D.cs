using UnityEngine;

public class LRigidBody2D : MonoBehaviour
{
    [SerializeField]
    private bool mIsFixed = false;

    [SerializeField]
    private LFloat mMass = 1;

    [SerializeField]
    private LFloat mMassInverse = 1;

    [SerializeField]
    private LFloat mInertia = 1;

    [SerializeField]
    private LFloat mInertiaInverse = 1;

    [SerializeField]
    private LFloat mLinearDrag = 0;

    [SerializeField]
    private LFloat mAngularDrag = 0;

    [SerializeField]
    private LFloat mGravityScale = 1;

    [SerializeField]
    private LVector2 mCenterOfMass = LVector2.zero;

    private LVector2 mLinearVelocity = LVector2.zero;
    private LFloat mAngularVelocity = 0;

    private LVector2 mUserForce = LVector2.zero;
    private LFloat mUserTorque = 0;

    private bool mIsSleeping = false;
    internal LFloat idleTime = 0;
    internal LFloat sleepTime = 0;

    public bool isFixed
    {
        get
        {
            return mIsFixed;
        }
        set
        {
            mIsFixed = value;
            if (mIsFixed)
            {
                mLinearVelocity = LVector2.zero;
                mAngularVelocity = 0;
            }
        }
    }

    public LFloat mass
    {
        get
        {
            return mMass;
        }
        set
        {
            mMass = mMass < 0 ? 0 : value;
            mMassInverse = 1 / mMass;
        }
    }

    public LFloat inertia
    {
        get
        {
            return mInertia;
        }
        set
        {
            mInertia = mInertia < 0 ? 0 : value;
            mInertiaInverse = 1 / mInertia;
        }
    }

    public LFloat linearDrag
    {
        get
        {
            return mLinearDrag;
        }
        set
        {
            mLinearDrag = value;
        }
    }

    public LFloat angularDrag
    {
        get
        {
            return mAngularDrag;
        }
        set
        {
            mAngularDrag = value;
        }
    }

    public LFloat gravityScale
    {
        get
        {
            return mGravityScale;
        }
        set
        {
            mGravityScale = value;
        }
    }

    public LVector2 centerOfMass
    {
        get
        {
            return mCenterOfMass;
        }
        set
        {
            mCenterOfMass = value;
        }
    }

    public LVector2 linearVelocity
    {
        get
        {
            return mLinearVelocity;
        }
        set
        {
            mLinearVelocity = value;
        }
    }

    public LFloat angularVelocity
    {
        get
        {
            return mAngularVelocity;
        }
        set
        {
            mAngularVelocity = value;
        }
    }

    public LVector2 userForce
    {
        get
        {
            return mUserForce;
        }
    }

    public LFloat userTorque
    {
        get
        {
            return mUserTorque;
        }
    }

    public bool isSleeping
    {
        get
        {
            return mIsSleeping;
        }
        set
        {
            mIsSleeping = value;
        }
    }

    internal LFloat massInverse
    {
        get
        {
            return mMassInverse;
        }
    }

    internal LFloat inertiaInverse
    {
        get
        {
            return mInertiaInverse;
        }
    }

    public LVector2 GetWorldCenterOfMass()
    {
        return transform.TransformPoint(LParser.Parse(mCenterOfMass));
    }

    public void AddForce(LVector2 force, LForceMode2D forceMode)
    {
        switch (forceMode)
        {
            case LForceMode2D.Force:
                {
                    mUserForce += force;
                    break;
                }
            case LForceMode2D.Impulse:
                {
                    mLinearVelocity += force * mMassInverse;
                    break;
                }
        }
    }

    public void AddForceAtPosition(LVector2 force, LVector2 position, LForceMode2D forceMode)
    {
        AddForce(force, forceMode);
        switch (forceMode)
        {
            case LForceMode2D.Force:
                {
                    mUserTorque += LVector3.Cross(position - GetWorldCenterOfMass(), force).z;
                    break;
                }
            case LForceMode2D.Impulse:
                {
                    mAngularVelocity += LVector3.Cross(position - GetWorldCenterOfMass(), force).z * mInertiaInverse;
                    break;
                }
        }
    }

    public void AddRelativeForce(LVector2 relativeForce, LVector2 localPosition, LForceMode2D forceMode)
    {
        LVector3 globalForce = transform.rotation * (LVector3)relativeForce;
        LVector2 globalPosition = transform.TransformPoint(localPosition);
        AddForceAtPosition(globalForce, globalPosition, forceMode);
    }

    public void AddTorque(LFloat torque, LForceMode2D forceMode)
    {
        switch (forceMode)
        {
            case LForceMode2D.Force:
                {
                    mUserTorque += torque;
                    break;
                }
            case LForceMode2D.Impulse:
                {
                    mAngularVelocity += torque * inertiaInverse;
                    break;
                }
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

    internal LFloat[] GetFirstOrder()
    {
        Vector3 p = transform.position;
        LFloat q = transform.eulerAngles.z * Mathf.Deg2Rad;
        return new LFloat[] 
        {
            p.x,
            p.y,
            q
        };
    }

    internal LFloat[] GetFirstOrderDerivatives(LFloat t)
    {
        return new LFloat[]
        {
            mLinearVelocity.x,
            mLinearVelocity.y,
            mAngularVelocity
        };
    }

    internal void ApplyFirstOrder(LFloat[] x)
    {
        transform.position = new LVector2(x[0], x[1]);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, x[2] * Mathf.Rad2Deg);
    }

    internal void ApplyForceEffect(LFloat deltaTime)
    {
        bool sliding = mLinearVelocity.sqrMagnitude > 0;
        bool rotating = mAngularVelocity > 0;

        // linear velocity
        LVector2 va = mUserForce * mMassInverse;
        if (!mIsFixed)
        {
            va += LPhysicsStatic2D.LGravity * mGravityScale;
        }

        if (sliding && mLinearDrag > 0)
        {
            LFloat da = mLinearDrag * mMassInverse;
            LFloat minr = (mLinearVelocity / deltaTime).sqrMagnitude;
            if (da * da >= minr)
            {
                va = LVector2.zero;
                mLinearVelocity = LVector2.zero;
            }
            else
            {
                va -= da * mLinearVelocity.normalized;
            }
        }
        if (va != LVector2.zero)
        {
            mLinearVelocity += va * deltaTime;
        }

        // angular velocity
        LFloat ra = mUserTorque * mInertiaInverse;
        if (rotating && mAngularDrag > 0)
        {
            LFloat da = mAngularDrag * mInertiaInverse;
            LFloat minr = mAngularVelocity / deltaTime;
            if (da >= minr)
            {
                ra = 0;
                mAngularVelocity = 0;
            }
            else
            {
                ra -= da * LMath.Sign(ra);
            }
        }
        if (ra != 0)
        {
            mAngularVelocity += ra * deltaTime;
        }
    }
}
