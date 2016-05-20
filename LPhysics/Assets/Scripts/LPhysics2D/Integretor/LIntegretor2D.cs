using System;
using System.Collections.Generic;
using UnityEngine;

internal abstract class LIntegretor2D
{
    public void Integrete(List<LPhysicsObject2D> objs, LFloat t0, LFloat t1)
    {
        for (int i = objs.Count - 1; i >= 0; i--)
        {
            if (objs[i].rigidBody != null)
            {
                LFloat[] x = Integrete(objs[i].rigidBody.GetFirstOrder(), t0, t1, objs[i].rigidBody.GetFirstOrderDerivatives);
                objs[i].rigidBody.ApplyFirstOrder(x);
            }
        }
    }

    protected abstract LFloat[] Integrete(LFloat[] x, LFloat t0, LFloat t1, Func<LFloat, LFloat[]> GetDxDtMethod);
}
