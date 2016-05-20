using UnityEngine;

internal sealed class LGeometryUtil2D
{
    /// <summary>
    /// 计算v1v2的垂线，pointTo决定垂线的指向(返回向量未归一化)
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="pointTo"></param>
    public static LVector2 GetPerpendicular2D(LVector2 v1, LVector2 v2, LVector2 pointTo)
    {
        LVector2 v12 = v1 - v2;
        LVector2 dir = new LVector2(-v12.y, v12.x);
        int sign = LVector2.Dot(dir, pointTo - v1) > 0 ? 1 : -1;
        dir *= sign;
        return dir;
    }

    /// <summary>
    /// 计算向量v的垂线
    /// </summary>
    /// <param name="v"></param>
    /// <param name="counterClockWise"></param>
    /// <returns></returns>
    public static LVector2 GetPerpendicular2D(LVector2 v, bool counterClockWise = true)
    {
        return new LVector2(-v.y, v.x) * (counterClockWise ? 1 : -1);
    }

    /// <summary>
    /// 计算顶点集的区域
    /// </summary>
    /// <param name="vertics"></param>
    /// <returns></returns>
    public static Bounds CalcBounds(LVector2[] vertics)
    {
        LVector2 min = new LVector2(LFloat.PositiveInfinity, LFloat.PositiveInfinity);
        LVector2 max = new LVector2(LFloat.NegativeInfinity, LFloat.NegativeInfinity);
        for (int i = vertics.Length - 1; i >= 0; i--)
        {
            if (vertics[i].x < min.x)
            {
                min.x = vertics[i].x;
            }
            if (vertics[i].y < min.y)
            {
                min.y = vertics[i].y;
            }
            if (vertics[i].x > max.x)
            {
                max.x = vertics[i].x;
            }
            if (vertics[i].y > max.y)
            {
                max.y = vertics[i].y;
            }
        }
        Bounds bounds = new Bounds();
        bounds.min = min;
        bounds.max = max;
        return bounds;
    }

    /// <summary>
    /// 重心坐标
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static LVector2 CalcBarycentric(LVector2 a, LVector2 b)
    {
        return (a + b) * 0.5d;
    }
}
