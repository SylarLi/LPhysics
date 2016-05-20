using System.Collections.Generic;
using UnityEngine;

public class LPhysics2D : Singleton<LPhysics2D>
{
    public bool simulate = true;

    private List<int> physicsIndexes;
    private List<LPhysicsObject2D> physicsObjs;

    private LTimeStepStrategy2D timeStepStrategy;

    public LPhysics2D()
    {
        physicsIndexes = new List<int>();
        physicsObjs = new List<LPhysicsObject2D>();
        timeStepStrategy = new LExplicitTimeStepStrategy2D();
    }

    public void Update(Transform trans)
    {
        int instanceID = trans.GetInstanceID();
        int index = physicsIndexes.IndexOf(instanceID);
        LPhysicsObject2D physicsObj = null;
        if (index == -1)
        {
            physicsObj = new LPhysicsObject2D(trans);
            physicsIndexes.Add(instanceID);
            physicsObjs.Add(physicsObj);
        }
        else
        {
            physicsObj = physicsObjs[index];
        }
        physicsObj.Refresh();
        if (physicsObj.collider == null &&
            physicsObj.rigidBody == null)
        {
            physicsObjs.Remove(physicsObj);
        }
    }

    public void Simulate(LFloat deltaTime)
    {
        if (simulate)
        {
            timeStepStrategy.Step(physicsObjs, deltaTime);
        }
    }
}
