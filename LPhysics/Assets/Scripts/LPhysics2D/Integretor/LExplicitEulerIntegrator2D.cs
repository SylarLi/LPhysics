using UnityEngine;
using System;

internal class LExplicitEulerIntegrator2D : LIntegretor2D
{
    protected override LFloat[] Integrete(LFloat[] x, LFloat t0, LFloat t1, Func<LFloat, LFloat[]> GetDxDtMethod)
    {
        LFloat[] result = new LFloat[x.Length];
        LFloat[] dxdts = GetDxDtMethod(t0);
        LFloat dt = t1 - t0;
        for (int i = result.Length - 1; i >= 0; i--)
        {
            result[i] = x[i] + dt * dxdts[i];
        }
        return result;
    }
}
