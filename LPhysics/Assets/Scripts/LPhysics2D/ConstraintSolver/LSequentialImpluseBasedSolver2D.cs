using System.Collections.Generic;

internal class LSequentialImpluseBasedSolver2D : LConstraintSolver2D
{
    public override void Solve(List<LConstraint2D> constraints, float deltaTime)
    {
        for (int i = constraints.Count - 1; i >= 0; i--)
        {
            constraints[i].ApplyConstraintImpluse(deltaTime);
        }
        for (int i = constraints.Count - 1; i >= 0; i--)
        {
            constraints[i].ApplyPositionCorrection(deltaTime);
        }
    }
}
