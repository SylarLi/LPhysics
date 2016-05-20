using UnityEngine;

public class LPhysicsTimer2D : MonoBehaviour
{
    private void FixedUpdate()
    {
        LPhysics2D.inst.Simulate(Time.fixedDeltaTime);
    }
}
