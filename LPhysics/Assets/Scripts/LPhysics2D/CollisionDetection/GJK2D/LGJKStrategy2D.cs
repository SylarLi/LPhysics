using System.Collections.Generic;
using UnityEngine;

internal class LGJKStrategy2D : LCollisionDetectionStrategy2D
{
    private Simplex2D simplex;

    private Vector2 direction;

    private EPA2D epa;

    private EPAFace2D closestFace;

    private List<EPAFace2D> safety;

    public LGJKStrategy2D()
    {
        simplex = new Simplex2D();
        epa = new EPA2D();
        safety = new List<EPAFace2D>();
    }

    public override void CheckForCollisions(List<LPhysicsObject2D> objs, float deltaTime)
    {
        base.CheckForCollisions(objs, deltaTime);
        for (int i = objs.Count - 1; i >= 1; i--)
        {
            for (int j = i - 1; j >= 0; j--)
            {
                LPhysicsObject2D objA = objs[i];
                LPhysicsObject2D objB = objs[j];
                if (objA.collider != null &&
                    objB.collider != null &&
                    objA.collider.bounds.Intersects(objB.collider.bounds))
                {
                    simplex.Clear();
                    direction = Vector2.one;
                    epa.Clear();
                    closestFace = null;
                    safety.Clear();
                    LContactConstraint2D constraint = CollisionDetect(objA, objB);
                    if (constraint != null)
                    {
                        contactConstraints.Add(constraint);
                    }
                }
            }
        }
    }

    /// <summary>
    /// <para>GJK判断是否碰撞</para>
    /// <para>1.找direction方向的最远点</para>
    /// <para>2.判断最远点是否与原点在同一方向(是否比原点还远)，如果不是则未碰撞</para>
    /// <para>3.将最远点置入simplex，判断simplex是否包围了原点，如果是则碰撞，否则以离原点最近的simplex上的点到原点作为direction回到步骤1</para>
    /// </summary>
    /// <param name="objA"></param>
    /// <param name="objB"></param>
    private LContactConstraint2D CollisionDetect(LPhysicsObject2D objA, LPhysicsObject2D objB)
    {
        Vector2 a = objA.collider.GetFurtherPointInDirection(direction);
        Vector2 b = objB.collider.GetFurtherPointInDirection(-direction);
        SimplexPoint2D sp = new SimplexPoint2D(a, b);
        if (simplex.IsSameDirectionWithOrigin(sp, direction))
        {
            simplex.Add(sp);
            if (simplex.DoSimplex2D())
            {
                epa.Add(new EPAFace2D(simplex[0], simplex[1]));
                epa.Add(new EPAFace2D(simplex[1], simplex[2]));
                epa.Add(new EPAFace2D(simplex[2], simplex[0]));
                PenetrationDetect(objA, objB);
                return ContactConstraintGenerate(objA, objB);
            }
            else
            {
                direction = simplex.FindClosestPointToOriginDirection();
                return CollisionDetect(objA, objB);
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// <para>EPA判断渗透方向和深度</para>
    /// <para>1.找到list中离原点最近的face</para>
    /// <para>2.如果与上一次找到的相同，直接返回</para>
    /// <para>3.否则找face.normal的最远点A</para>
    /// <para>4.移除使A看不到的原点的face，将(A, face.A)和(A, face.B)构建为新的face添加到list中，返回第一步</para>
    /// </summary>
    /// <returns></returns>
    private void PenetrationDetect(LPhysicsObject2D objA, LPhysicsObject2D objB)
    {
        EPAFace2D face = epa.FindClosestFace();
        if (closestFace == null ||
            Mathf.Abs(closestFace.sqrMagnitude - face.sqrMagnitude) > EPAFace2D.Threshold)
        {
            closestFace = face;
            safety.Add(closestFace);
            Vector2 a = objA.collider.GetFurtherPointInDirection(closestFace.direction);
            Vector2 b = objB.collider.GetFurtherPointInDirection(-closestFace.direction);
            SimplexPoint2D sp = new SimplexPoint2D(a, b);
            epa.ImportNewSupportPoint(sp);
            PenetrationDetect(objA, objB);
        }
    }

    private LContactConstraint2D ContactConstraintGenerate(LPhysicsObject2D objA, LPhysicsObject2D objB)
    {
        LContactConstraint2D constraint = null;
        Vector2 contactPointsA = LGeometryUtil2D.CalcBarycentric(closestFace.sp1.a, closestFace.sp2.a);
        Vector2 contactPointsB = LGeometryUtil2D.CalcBarycentric(closestFace.sp1.b, closestFace.sp2.b);
        if (closestFace.sqrMagnitude > LPhysicsStatic2D.DistanceSqrtThreshold)
        {
            float penetration = Mathf.Sqrt(closestFace.sqrMagnitude);
            Vector2 normal = closestFace.direction / penetration;
            if (objA.rigidBody != null ||
                objB.rigidBody != null)
            {
                constraint = new LContactConstraint2D(objA, objB);
                constraint.anchorA = contactPointsA;
                constraint.anchorB = contactPointsB;
                constraint.normal = normal;
                constraint.penetration = penetration;
            }
        }
        return constraint;
    }
}
