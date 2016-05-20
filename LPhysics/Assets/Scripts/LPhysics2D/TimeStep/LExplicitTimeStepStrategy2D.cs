using System.Collections.Generic;

internal class LExplicitTimeStepStrategy2D : LTimeStepStrategy2D
{
    public override void Step(List<LPhysicsObject2D> objs, float deltaTime)
    {
        for (int i = objs.Count - 1; i >= 0; i--)
        {
            if (objs[i].rigidBody != null)
            {
                objs[i].rigidBody.ApplyForceEffect(deltaTime);
            }
        }
        collisionDetection.CheckForCollisions(objs, deltaTime);
        contactSolver.Solve(collisionDetection.contactConstraints, deltaTime);

        //...
        integretor.Integrete(objs, 0, deltaTime);
    }
}
