using UnityEngine;

/// <summary>
/// 常规约束(自定义)
/// </summary>
internal class LGeneralConstraint2D : LConstraint2D
{
    public LGeneralConstraint2D(LPhysicsObject2D objA, LPhysicsObject2D objB)
        : base(objA, objB)
    {

    }

    public override LConstraint2DType type
    {
        get
        {
            return LConstraint2DType.General;
        }
    }

    public override void ApplyConstraintImpluse(float deltaTime)
    {
        
    }

    public override void ApplyPositionCorrection(float deltaTime)
    {
        
    }
}
