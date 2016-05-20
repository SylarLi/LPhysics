public struct LVector2
{
    public LFloat x;

    public LFloat y;

    public LVector2(LFloat x, LFloat y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return string.Format("LVector2({0}, {1})", x, y);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return this == (LVector2)obj;
    }

    public void Normalize()
    {
        if (this != LVector2.zero &&
            sqrMagnitude != 1)
        {
            LFloat size = magnitude;
            x /= size;
            y /= size;
        }
    }

    public LFloat Dot(LVector2 v)
    {
        return LVector2.Dot(this, v);
    }

    public LFloat Cross(LVector2 v)
    {
        return LVector2.Cross(this, v);
    }

    public static LVector2 operator -(LVector2 a)
    {
        return new LVector2(-a.x, -a.y);
    }

    public static LVector2 operator -(LVector2 a, LVector2 b)
    {
        return new LVector2(a.x - b.x, a.y - b.y);
    }

    public static bool operator ==(LVector2 lhs, LVector2 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(LVector2 lhs, LVector2 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public static LVector2 operator *(LFloat d, LVector2 a)
    {
        return a * d;
    }

    public static LVector2 operator *(LVector2 a, LFloat d)
    {
        return new LVector2(a.x * d, a.y * d);
    }

    public static LVector2 operator /(LVector2 a, LFloat d)
    {
        return new LVector2(a.x / d, a.y / d);
    }

    public static LVector2 operator +(LVector2 a, LVector2 b)
    {
        return new LVector2(a.x + b.x, a.y + b.y);
    }

    public static implicit operator LVector3(LVector2 v)
    {
        return new LVector3(v.x, v.y, 0);
    }

    public static implicit operator LVector2(LVector3 v)
    {
        return new LVector2(v.x, v.y);
    }

    public LFloat sqrMagnitude
    {
        get
        {
            return x * x + y * y;
        }
    }

    public LFloat magnitude
    {
        get
        {
            return LMath.Sqrt(sqrMagnitude);
        }
    }

    public LVector2 normalized
    {
        get
        {
            if (this == LVector2.zero)
            {
                return LVector2.zero;
            }
            else if (sqrMagnitude == 1)
            {
                return this;
            }
            else
            {
                return this / magnitude;
            }
        }
    }

    public static LVector2 one
    {
        get
        {
            return new LVector2(1, 1);
        }
    }

    public static LVector2 right
    {
        get
        {
            return new LVector2(1, 0);
        }
    }

    public static LVector2 up
    {
        get
        {
            return new LVector2(0, 1);
        }
    }

    public static LVector2 zero
    {
        get
        {
            return new LVector2();
        }
    }

    public static LFloat Dot(LVector2 lhs, LVector2 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y;
    }

    public static LFloat Cross(LVector2 lhs, LVector2 rhs)
    {
        return lhs.x * rhs.y - lhs.y * rhs.x;
    }

    public static LVector2 Project(LVector2 vec, LVector2 on)
    {
        return vec.Dot(on) / on.sqrMagnitude * on;
    }

    public static LFloat Angle(LVector2 lhs, LVector2 rhs)
    {
        lhs.Normalize();
        rhs.Normalize();
        return LMath.Acos(lhs.Dot(rhs)) * LMath.RadToDeg;
    }

    public static LVector2 Lerp(LVector2 from, LVector2 to, float t)
    {
        return (1 - t) * from + t * to;
    }

    public static LVector2 Slerp(LVector2 from, LVector2 to, LFloat t)
    {
        t = LMath.Clamp(t, 0, 1);
        LFloat diff = Angle(from, to) * LMath.DegToRad;
        LFloat sind = LMath.Sin(diff);
        LFloat sintd = LMath.Sin(t * diff);
        LFloat sin1td = LMath.Sin((1 - t) * diff);
        return (sin1td / sind) * from + (sintd / sind) * to;
    }
}
