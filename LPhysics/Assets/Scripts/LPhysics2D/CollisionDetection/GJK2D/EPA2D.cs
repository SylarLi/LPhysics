using System.Collections.Generic;
using UnityEngine;

internal class EPA2D
{
    private List<EPAFace2D> faces;

    private List<SimplexPoint2D> points;

    public EPA2D()
    {
        faces = new List<EPAFace2D>();
        points = new List<SimplexPoint2D>();
    }

    public void Add(EPAFace2D face)
    {
        faces.Add(face);
    }

    public void Clear()
    {
        faces.Clear();
    }

    /// <summary>
    /// 寻找距离原点最近的face
    /// </summary>
    /// <returns></returns>
    public EPAFace2D FindClosestFace()
    {
        EPAFace2D minFace = null;
        for (int i = faces.Count - 1; i >= 0; i--)
        {
            if (minFace == null ||
                faces[i].sqrMagnitude < minFace.sqrMagnitude)
            {
                minFace = faces[i];
            }
        }
        return minFace;
    }

    /// <summary>
    /// 引入一个新的support point，删除使得此点看不见原点的edge，将删除的edge的点保存在list中
    /// 剔除list中的重复点，用list中的每个点与sp构建新的edge
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="sp"></param>
    public void ImportNewSupportPoint(SimplexPoint2D sp)
    {
        for (int i = faces.Count - 1; i >= 0; i--)
        {
            if (LVector2.Dot(sp.p - faces[i].sp1.p, faces[i].direction) > 0)
            {
                AddExpiredEdgePoint(faces[i].sp1);
                AddExpiredEdgePoint(faces[i].sp2);
                faces.RemoveAt(i);
            }
        }
        for (int i = points.Count - 1; i >= 0; i--)
        {
            faces.Add(new EPAFace2D(sp, points[i]));
        }
        points.Clear();
    }

    public void AddExpiredEdgePoint(SimplexPoint2D sp)
    {
        for (int i = points.Count - 1; i >= 0; i--)
        {
            if (points[i].Equals(sp))
            {
                points.RemoveAt(i);
                return;
            }
        }
        points.Add(sp);
    }
}
