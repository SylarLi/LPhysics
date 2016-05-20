public struct LVector4
{
    public LFloat x;

    public LFloat y;

    public LFloat z;

    public LFloat w;

    public LVector4(LFloat x, LFloat y, LFloat z, LFloat w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public override string ToString()
    {
        return string.Format("LVector4({0}, {1}, {2}, {3})", x, y, z, w);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return this == (LVector4)obj;
    }

    public void Normalize()
    {
        if (this != LVector4.zero &&
            sqrMagnitude != 1)
        {
            LFloat size = magnitude;
            x /= size;
            y /= size;
            z /= size;
            w /= size;
        }
    }

    public LFloat Dot(LVector4 v)
    {
        return LVector4.Dot(this, v);
    }

    public static LVector4 operator -(LVector4 a)
    {
        return new LVector4(-a.x, -a.y, -a.z, -a.w);
    }

    public static LVector4 operator -(LVector4 a, LVector4 b)
    {
        return new LVector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }

    public static bool operator ==(LVector4 lhs, LVector4 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w;
    }

    public static bool operator !=(LVector4 lhs, LVector4 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.w != rhs.w;
    }

    public static LVector4 operator *(LFloat d, LVector4 a)
    {
        return a * d;
    }

    public static LVector4 operator *(LVector4 a, LFloat d)
    {
        return new LVector4(a.x * d, a.y * d, a.z * d, a.w * d);
    }

    public static LVector4 operator /(LVector4 a, LFloat d)
    {
        return new LVector4(a.x / d, a.y / d, a.z / d, a.w / d);
    }

    public static LVector4 operator +(LVector4 a, LVector4 b)
    {
        return new LVector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    }

    public LFloat sqrMagnitude
    {
        get
        {
            return x * x + y * y + z * z + w * w;
        }
    }

    public LFloat magnitude
    {
        get
        {
            return LMath.Sqrt(sqrMagnitude);
        }
    }

    public LVector4 normalized
    {
        get
        {
            if (this == LVector4.zero)
            {
                return LVector4.zero;
            }
            else
            {
                LFloat size = magnitude;
                return this / size;
            }
        }
    }

    public static LVector4 one
    {
        get
        {
            return new LVector4(1, 1, 1, 1);
        }
    }

    public static LVector4 zero
    {
        get
        {
            return new LVector4();
        }
    }

    public static LFloat Dot(LVector4 lhs, LVector4 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;
    }

    public static LVector4 Project(LVector4 vec, LVector4 on)
    {
        return vec.Dot(on) / on.sqrMagnitude * on;
    }

    public static LFloat Angle(LVector4 lhs, LVector4 rhs)
    {
        lhs.Normalize();
        rhs.Normalize();
        return LMath.Acos(lhs.Dot(rhs)) * LMath.RadToDeg;
    }

    public static LVector4 Lerp(LVector4 from, LVector4 to, float t)
    {
        return (1 - t) * from + t * to;
    }

    public static LVector4 Slerp(LVector4 from, LVector4 to, LFloat t)
    {
        t = LMath.Clamp(t, 0, 1);
        LFloat diff = Angle(from, to) * LMath.DegToRad;
        LFloat sind = LMath.Sin(diff);
        LFloat sintd = LMath.Sin(t * diff);
        LFloat sin1td = LMath.Sin((1 - t) * diff);
        return (sin1td / sind) * from + (sintd / sind) * to;
    }
}
