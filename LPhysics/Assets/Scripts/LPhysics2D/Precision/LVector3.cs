public struct LVector3
{
    public LFloat x;

    public LFloat y;

    public LFloat z;

    public LVector3(LFloat x, LFloat y, LFloat z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override string ToString()
    {
        return string.Format("LVector3({0}, {1}, {2})", x, y, z);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return this == (LVector3)obj;
    }

    public void Normalize()
    {
        if (this != LVector3.zero &&
            sqrMagnitude != 1)
        {
            LFloat size = magnitude;
            x /= size;
            y /= size;
            z /= size;
        }
    }

    public LFloat Dot(LVector3 v)
    {
        return LVector3.Dot(this, v);
    }

    public LVector3 Cross(LVector3 v)
    {
        return LVector3.Cross(this, v);
    }

    public static LVector3 operator -(LVector3 a)
    {
        return new LVector3(-a.x, -a.y, -a.z);
    }

    public static LVector3 operator -(LVector3 a, LVector3 b)
    {
        return new LVector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static bool operator !=(LVector3 lhs, LVector3 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
    }

    public static LVector3 operator *(LFloat d, LVector3 a)
    {
        return a * d;
    }

    public static LVector3 operator *(LVector3 a, LFloat d)
    {
        return new LVector3(a.x * d, a.y * d, a.z * d);
    }

    public static LVector3 operator /(LVector3 a, LFloat d)
    {
        return new LVector3(a.x / d, a.y / d, a.z / d);
    }

    public static LVector3 operator +(LVector3 a, LVector3 b)
    {
        return new LVector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static bool operator ==(LVector3 lhs, LVector3 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
    }

    public LFloat sqrMagnitude
    {
        get
        {
            return x * x + y * y + z * z;
        }
    }

    public LFloat magnitude
    {
        get
        {
            return LMath.Sqrt(sqrMagnitude);
        }
    }

    public LVector3 normalized
    {
        get
        {
            if (this == LVector3.zero)
            {
                return LVector3.zero;
            }
            else
            {
                LFloat size = magnitude;
                return this / size;
            }
        }
    }

    public static LVector3 one
    {
        get
        {
            return new LVector3(1, 1, 1);
        }
    }

    public static LVector3 right
    {
        get
        {
            return new LVector3(1, 0, 0);
        }
    }

    public static LVector3 up
    {
        get
        {
            return new LVector3(0, 1, 0);
        }
    }

    public static LVector3 forward
    {
        get
        {
            return new LVector3(0, 0, 1);
        }
    }

    public static LVector3 zero
    {
        get
        {
            return new LVector3();
        }
    }

    public static LFloat Dot(LVector3 lhs, LVector3 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    }

    public static LVector3 Cross(LVector3 lhs, LVector3 rhs)
    {
        return new LVector3(
            lhs.y * rhs.z - lhs.z * rhs.y, 
            lhs.z * rhs.x - lhs.x * rhs.z, 
            lhs.x * rhs.y - lhs.y * rhs.x
        );
    }

    public static LVector2 Project(LVector3 vec, LVector3 on)
    {
        return vec.Dot(on) / on.sqrMagnitude * on;
    }

    public static LFloat Angle(LVector3 lhs, LVector3 rhs)
    {
        lhs.Normalize();
        rhs.Normalize();
        return LMath.Acos(lhs.Dot(rhs)) * LMath.RadToDeg;
    }

    public static LVector3 Lerp(LVector3 from, LVector3 to, float t)
    {
        return (1 - t) * from + t * to;
    }

    public static LVector3 Slerp(LVector3 from, LVector3 to, LFloat t)
    {
        t = LMath.Clamp(t, 0, 1);
        LFloat diff = Angle(from, to) * LMath.DegToRad;
        LFloat sind = LMath.Sin(diff);
        LFloat sintd = LMath.Sin(t * diff);
        LFloat sin1td = LMath.Sin((1 - t) * diff);
        return (sin1td / sind) * from + (sintd / sind) * to;
    }
}
