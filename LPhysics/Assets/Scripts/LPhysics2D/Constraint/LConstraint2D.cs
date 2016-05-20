using UnityEngine;

internal abstract class LConstraint2D
{
    private LPhysicsObject2D mObjA;
    private LPhysicsObject2D mObjB;

    public LVector2 anchorA;
    public LVector2 anchorB;

    public LConstraint2D(LPhysicsObject2D objA, LPhysicsObject2D objB)
    {
        mObjA = objA;
        mObjB = objB;
    }

    public virtual LConstraint2DType type
    {
        get
        {
            return LConstraint2DType.None;
        }
    }

    public LPhysicsObject2D objA
    {
        get
        {
            return mObjA;
        }
    }

    public LPhysicsObject2D objB
    {
        get
        {
            return mObjB;
        }
    }

    public abstract void ApplyConstraintImpluse(LFloat deltaTime);


    public abstract void ApplyPositionCorrection(LFloat deltaTime);
}
