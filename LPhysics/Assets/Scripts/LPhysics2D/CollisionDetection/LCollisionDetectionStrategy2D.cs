using System.Collections.Generic;

internal abstract class LCollisionDetectionStrategy2D
{
    public const LFloat Epsilon = 0.001f;

    private List<LConstraint2D> mContactConstraints;

    public LCollisionDetectionStrategy2D()
    {
        mContactConstraints = new List<LConstraint2D>();
    }

    public List<LConstraint2D> contactConstraints
    {
        get
        {
            return mContactConstraints;
        }
    }

    public virtual void CheckForCollisions(List<LPhysicsObject2D> objs, LFloat deltaTime)
    {
        mContactConstraints.Clear();
        for (int i = objs.Count - 1; i >= 0; i--)
        {
            if (objs[i].collider != null)
            {
                objs[i].collider.PrepareForCollision();
            }
        }
    }
}
