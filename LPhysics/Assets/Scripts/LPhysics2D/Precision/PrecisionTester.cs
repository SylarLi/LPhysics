using UnityEngine;

public class PrecisionTester : MonoBehaviour
{
    private void Start()
    {
        TestLFloat();
        TestLVector2();
        TestLVector3();
        TestMatrix();
    }

    private void TestLFloat()
    {
        LFloat f1 = 781.247d;
        LFloat f2 = 100.000d;
        LFloat f3 = 78124.7d;
        AssertTrue(f1 + f2 == 881.247d);
        AssertTrue(f1 * f2 == 78124.7d);
        AssertTrue(f1 / f2 == 7.8124d);
        AssertTrue(f1 - f2 == 681.247d);
        AssertTrue(-f1 == -781.247d);
        AssertTrue(f1 / 0 == LFloat.PositiveInfinity);
        AssertTrue(-f1 / 0 == LFloat.NegativeInfinity);
        AssertTrue(f1 == f2 == false);
        AssertTrue(f1 != f2 == true);
        AssertTrue(f1 > f2 == true);
        AssertTrue(f1 < f2 == false);
        AssertTrue(f1 >= f2 == true);
        AssertTrue(f1 <= f2 == false);
        AssertTrue(f1.ToDouble() == 781.247d);
        AssertTrue(f1.ToDouble() == f1);
    }

    private void TestLVector2()
    {
        LVector2 v1 = new LVector2(3.3d, 1.2d);
        LVector2 v2 = new LVector2(1.2d, 3.3d);
        AssertTrue(v1 != v2);
        AssertFalse(v1 == v2);
        AssertTrue(v1 + v2 == new LVector2(4.5d, 4.5d));
        AssertTrue(v1 - v2 == new LVector2(2.1d, -2.1d));
        AssertTrue(v1 * 2 == new LVector2(6.6d, 2.4d));
        AssertTrue(v1 / 3 == new LVector2(1.1d, 0.4d));
        AssertTrue(LVector2.zero.normalized == LVector2.zero);
        AssertTrue(LVector2.Dot(v1, v2) == 7.92d);
        Debug.Log(v1.sqrMagnitude);
        Debug.Log(v1.magnitude);
        Debug.Log(v1.normalized);
        Debug.Log(LVector2.Cross(v1, v2));
        Debug.Log("----------------------");
    }

    private void TestLVector3()
    {
        LVector3 v1 = new LVector3(3.3d, 1.2d, 1.5d);
        LVector3 v2 = new LVector3(1.2d, 3.3d, 1.5d);
        AssertTrue(v1 != v2);
        AssertFalse(v1 == v2);
        AssertTrue(v1 + v2 == new LVector3(4.5d, 4.5d, 3));
        AssertTrue(v1 - v2 == new LVector3(2.1d, -2.1d, 0));
        AssertTrue(v1 * 2 == new LVector3(6.6d, 2.4d, 3));
        AssertTrue(v1 / 3 == new LVector3(1.1d, 0.4d, 0.5d));
        AssertTrue(LVector3.zero.normalized == LVector3.zero);
        AssertTrue(LVector3.Dot(v1, v2) == 10.17d);
        AssertTrue(LVector3.Dot(LVector3.Cross(v1, v2), v2) == 0);
        Debug.Log(v1.sqrMagnitude);
        Debug.Log(v1.magnitude);
        Debug.Log(v1.normalized);
        Debug.Log("----------------------");
    }

    private void TestMatrix()
    {
        LMatrix matrix = new LMatrix(new LFloat[] {
            1, 2,
            3, 4,
            5, 6
        }, 3);
        Debug.Log(matrix);
        Debug.Log(matrix.Inverse() * matrix);
        Debug.Log(matrix.Transpose());
        Debug.Log(matrix * 3);
        Debug.Log("----------------------");
    }

    private void AssertTrue(bool result)
    {
        if (!result) Debug.LogError("Error");
    }

    private void AssertFalse(bool result)
    {
        if (result) Debug.LogError("Error");
    }
}
