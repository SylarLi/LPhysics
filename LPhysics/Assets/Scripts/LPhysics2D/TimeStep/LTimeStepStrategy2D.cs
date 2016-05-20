using System.Collections.Generic;

internal abstract class LTimeStepStrategy2D
{
    protected LCollisionDetectionStrategy2D collisionDetection;

    protected LConstraintSolver2D contactSolver;

    protected LConstraintSolver2D generalConstraintSolver;

    protected LIntegretor2D integretor;

    public LTimeStepStrategy2D()
    {
        collisionDetection = new LGJKStrategy2D();
        contactSolver = new LSequentialImpluseBasedSolver2D();
        //...
        integretor = new LExplicitEulerIntegrator2D();
    }

    public abstract void Step(List<LPhysicsObject2D> objs, LFloat deltaTime);
}
