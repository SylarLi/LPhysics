using UnityEngine;

internal class Simplex2D
{
    private SimplexPoint2D[] vertics;

    private int count;

    public Simplex2D()
    {
        vertics = new SimplexPoint2D[3];
        count = 0;
    }

    public SimplexPoint2D this[int index]
    {
        get
        {
            return vertics[index];
        }
    }

    /// <summary>
    /// 清除内容
    /// </summary>
    public void Clear()
    {
        count = 0;
    }

    /// <summary>
    /// 添加一个Support点
    /// </summary>
    /// <param name="sp"></param>
    public void Add(SimplexPoint2D sp)
    {
        vertics[count] = sp;
        count += 1;
    }

    /// <summary>
    /// 返回一个离原点最近的一个点到原点方向的向量
    /// <para>注:如果vertics中只有一个点，直接计算返回</para>
    /// <para>注:如果vertics中有两个点，向量为两点连线的垂线(指向原点的方向)</para>
    /// </summary>
    /// <returns></returns>
    public LVector2 FindClosestPointToOriginDirection()
    {
        LVector2 dir = LVector2.up;
        if (count == 1)
        {
            dir = -vertics[0].p;
        }
        else if (count == 2)
        {
            dir = LGeometryUtil2D.GetPerpendicular2D(vertics[0].p, vertics[1].p, LVector2.zero);
        }
        return dir;
    }

    /// <summary>
    /// 判断找到Support点是是否是沿找寻的方向
    /// <para>如果是相反的方向，说明不会包含原点，退出GJK</para>
    /// </summary>
    /// <returns></returns>
    public bool IsSameDirectionWithOrigin(SimplexPoint2D sp, LVector2 direction)
    {
        bool certain = false;
        if (count == 0)
        {
            certain = true;
        }
        else if (count == 1 ||
            count == 2)
        {
            certain = LVector2.Dot(sp.p, direction) >= 0;
        }
        return certain;
    }

    /// <summary>
    /// 判断是否包围了原点
    /// <para>如果没有包含，且点数超过2时，只保留连线离原点最近的两个点</para>
    /// </summary>
    /// <returns></returns>
    public bool DoSimplex2D()
    {
        bool certain = false;
        if (count == 1 || 
            count == 2)
        {
            certain = false;
        }
        else if (count == 3)
        {
            LVector2 p02 = LGeometryUtil2D.GetPerpendicular2D(vertics[0].p, vertics[2].p, vertics[1].p);
            if (LVector2.Dot(-vertics[0].p, p02) >= 0)
            {
                LVector2 p12 = LGeometryUtil2D.GetPerpendicular2D(vertics[1].p, vertics[2].p, vertics[0].p);
                if (LVector2.Dot(-vertics[1].p, p12) >= 0)
                {
                    certain = true;
                }
                else
                {
                    RemoveIndex(0);
                }
            }
            else
            {
                RemoveIndex(1);
            }
        }
        return certain;
    }

    /// <summary>
    /// 移除指定序号的点
    /// </summary>
    /// <param name="index"></param>
    private void RemoveIndex(int index)
    {
        for (int i = index; i < count - 1; i++)
        {
            vertics[i] = vertics[i + 1];
        }
        count -= 1;
    }
}
