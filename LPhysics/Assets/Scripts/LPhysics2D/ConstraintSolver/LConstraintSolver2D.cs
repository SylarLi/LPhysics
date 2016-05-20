using System.Collections.Generic;

internal abstract class LConstraintSolver2D
{
    public abstract void Solve(List<LConstraint2D> constraints, LFloat deltaTime);
}
