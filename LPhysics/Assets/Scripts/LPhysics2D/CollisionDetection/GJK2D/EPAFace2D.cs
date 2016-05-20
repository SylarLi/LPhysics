using UnityEngine;

internal class EPAFace2D
{
    public SimplexPoint2D sp1;

    public SimplexPoint2D sp2;

    public LVector2 direction;       // 由原点出发到AB的垂线(包含长度)

    public LFloat sqrMagnitude;      // 原点到直线的距离的平方

    public EPAFace2D(SimplexPoint2D sp1, SimplexPoint2D sp2)
    {
        this.sp1 = sp1;
        this.sp2 = sp2;
        // 这里使用计算直线交点的方向求得原点到直线的最短向量
        LVector2 D = -LGeometryUtil2D.GetPerpendicular2D(sp1.p, sp2.p, LVector2.zero);
        if (sp2.p.x == sp1.p.x)
        {
            LFloat x = sp1.p.x;
            this.direction = new LVector2(x, 0);
            this.sqrMagnitude = x * x;
        }
        else
        {
            if (D.x == 0)
            {
                LFloat y = sp1.p.y;
                this.direction = new LVector2(0, y);
                this.sqrMagnitude = y * y;
            }
            else
            {
                LFloat k1 = (sp2.p.y - sp1.p.y) / (sp2.p.x - sp1.p.x);
                LFloat b1 = (sp2.p.y * sp1.p.x - sp2.p.x * sp1.p.y) / (sp1.p.x - sp2.p.x);
                LFloat k2 = D.y / D.x;
                LFloat b2 = 0;
                LFloat y = (b1 * k2 - b2 * k1) / (k2 - k1);
                LFloat x = y / k2;
                this.direction = new LVector2(x, y);
                this.sqrMagnitude = x * x + y * y;
            }
        }
    }
}