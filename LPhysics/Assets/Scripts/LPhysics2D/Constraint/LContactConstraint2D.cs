using UnityEngine;

/// <summary>
/// 碰撞约束
/// </summary>
internal class LContactConstraint2D : LConstraint2D
{
    public LVector2 normal;

    public LFloat penetration;

    /// <summary>
    /// Error Reduce Parameter
    /// </summary>
    public LFloat ERP = 0.5d;

    public LContactConstraint2D(LPhysicsObject2D objA, LPhysicsObject2D objB)
        : base(objA, objB)
    {

    }

    public override LConstraint2DType type
    {
        get
        {
            return LConstraint2DType.Contact;
        }
    }

    public override void ApplyConstraintImpluse(LFloat deltaTime)
    {
        LVector2 ca = objA.GetWorldCenterOfMass();
        LVector2 ra = anchorA - ca;
        LVector2 cb = objB.GetWorldCenterOfMass();
        LVector2 rb = anchorB - cb;

        // 1 X 6
        LMatrix jacobian = new LMatrix(new LFloat[] {
            -normal.x, -normal.y, -LVector3.Cross(ra, normal).z, normal.x, normal.y, LVector3.Cross(rb, normal).z
        }, 1);

        // 6 X 1
        LMatrix jacobian_t = jacobian.Transpose();

        // 6 X 6
        LMatrix massMatrixInverse = new LMatrix(6, 6);
        massMatrixInverse[0][0] = objA.massInverse;
        massMatrixInverse[1][1] = objA.massInverse;
        massMatrixInverse[2][2] = objA.inertiaInverse;
        massMatrixInverse[3][3] = objB.massInverse;
        massMatrixInverse[4][4] = objB.massInverse;
        massMatrixInverse[5][5] = objB.inertiaInverse;

        // 6 X 1
        LMatrix v = new LMatrix(new LFloat[] {
            objA.linearVelocity.x, 
            objA.linearVelocity.y, 
            objA.angularVelocity,
            objB.linearVelocity.x, 
            objB.linearVelocity.y, 
            objB.angularVelocity,
        }, 6);

        //GeneralMatrix m1 = jacobian * massMatrixInverse;
        //GeneralMatrix m2 = jacobian_t;
        //double[][] m1s = m1.Array;
        //Debug.Log(m1s[0][0] + " " + m1s[0][1] + " " + m1s[0][2] + " " + m1s[0][3] + " " + m1s[0][4] + " " + m1s[0][5]);
        //double[][] m2s = m1.Array;
        //Debug.Log(m2s[0][0] + " " + m2s[1][0] + " " + m2s[2][0] + " " + m2s[3][0] + " " + m2s[4][0] + " " + m2s[5][0]);

        LMatrix lamdaMatrix = (jacobian * v * -1) * (jacobian * massMatrixInverse * jacobian_t).Inverse();
        LFloat lamda = lamdaMatrix[0][0];

        // 6 X 1
        LMatrix dv = massMatrixInverse * jacobian_t * lamda;

        objA.linearVelocity += new LVector2(dv[0][0], dv[1][0]);
        objA.angularVelocity += dv[2][0];
        objB.linearVelocity += new LVector2(dv[3][0], dv[4][0]);
        objB.angularVelocity += dv[5][0];
    }

    public override void ApplyPositionCorrection(float deltaTime)
    {
        if ((objA.linearVelocity - objB.linearVelocity).sqrMagnitude <= 0)
        {
            LVector2 ca = objA.GetWorldCenterOfMass();
            LVector2 ra = anchorA - ca;
            LVector2 cb = objB.GetWorldCenterOfMass();
            LVector2 rb = anchorB - cb;

            LFloat pa = objA.mass / (objA.mass + objB.mass);
            LVector3 dva = -(LVector3)normal * penetration * pa * ERP;
            LVector3 dvb = (LVector3)normal * penetration * (1 - pa) * ERP;
            LFloat dwa = LVector3.Cross(ra, dva * objA.mass).z * objA.inertiaInverse * deltaTime;
            LFloat dwb = LVector3.Cross(rb, dvb * objB.mass).z * objB.inertiaInverse * deltaTime;

            if (objA.rigidBody != null && !objA.rigidBody.isFixed)
            {
                objA.transform.position += LParser.Parse(dva);
                objA.transform.eulerAngles += new Vector3(0, 0, (dwa * Mathf.Rad2Deg).ToFloat());
            }
            if (objB.rigidBody != null && !objB.rigidBody.isFixed)
            {
                objB.transform.position += LParser.Parse(dvb);
                objB.transform.eulerAngles += new Vector3(0, 0, (dwb * Mathf.Rad2Deg).ToFloat());
            }
        }
    }
}
