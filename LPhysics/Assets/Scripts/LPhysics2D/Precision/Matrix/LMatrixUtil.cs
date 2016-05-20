internal sealed class LMatrixUtil
{
    /// <summary>
    ///  sqrt(a^2 + b^2) without under/overflow.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>

    public static LFloat Hypot(LFloat a, LFloat b)
    {
        LFloat r;
        if (LMath.Abs(a) > LMath.Abs(b))
        {
            r = b / a;
            r = LMath.Abs(a) * LMath.Sqrt(1 + r * r);
        }
        else if (b != 0)
        {
            r = a / b;
            r = LMath.Abs(b) * LMath.Sqrt(1 + r * r);
        }
        else
        {
            r = 0d;
        }
        return r;
    }
}
