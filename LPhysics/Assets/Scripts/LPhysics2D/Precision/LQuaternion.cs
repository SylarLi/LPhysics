public struct LQuaternion
{
    public LFloat w;
    public LFloat x;
    public LFloat y;
    public LFloat z;

    public LQuaternion(LFloat x, LFloat y, LFloat z, LFloat w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format("Quaternion({0}, {1}, {2}, {3})", x, y, z, w);
    }

    public override bool Equals(object other)
    {
        return this == (LQuaternion)other;
    }

    public void Normalize()
    {
        LFloat sqrtm = sqrtMagnitude;
        if (sqrtm != 1)
        {
            LFloat m = LMath.Sqrt(sqrtm);
            x /= m;
            y /= m;
            z /= m;
            w /= m;
        }
    }

    public LQuaternion conjugation
    {
        get
        {
            return new LQuaternion(-x, -y, -z, w);
        }
    }

    public LFloat sqrtMagnitude
    {
        get
        {
            LFloat sqrtm = x * x + y * y + z * z + w * w;
            if (sqrtm == 0)
            {
                throw new System.InvalidOperationException("LQuaternion's sqrtMagnitude should not be zero.");
            }
            return sqrtm;
        }
    }

    public LFloat magnitude
    {
        get
        {
            return LMath.Sqrt(sqrtMagnitude);
        }
    }

    public static bool operator !=(LQuaternion lhs, LQuaternion rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.w != rhs.w;
    }

    public static bool operator ==(LQuaternion lhs, LQuaternion rhs)
    {
        return !(lhs != rhs);
    }

    public static LQuaternion operator *(LQuaternion lhs, LQuaternion rhs)
    {
        LFloat x = lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y;
        LFloat y = lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z;
        LFloat z = lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x;
        LFloat w = lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z;
        return new LQuaternion(x, y, z, w);
    }

    public static LQuaternion operator *(LQuaternion rotation, LFloat scalar)
    {
        return new LQuaternion(rotation.x * scalar, rotation.y * scalar, rotation.z * scalar, rotation.w * scalar);
    }

    public static LQuaternion operator *(LFloat scalar, LQuaternion rotation)
    {
        return rotation * scalar;
    }

    public static LQuaternion operator +(LQuaternion lhs, LQuaternion rhs)
    {
        return new LQuaternion(
            lhs.x + rhs.x,
            lhs.y + rhs.y,
            lhs.z + rhs.z,
            lhs.w + rhs.w
        );
    }

    public static LVector3 operator *(LQuaternion rotation, LVector3 point)
    {
        LQuaternion p = rotation * new LQuaternion(point.x, point.y, point.z, 0) * rotation.conjugation;
        return new LVector3(p.x, p.y, p.z);
    }

    public LVector3 eulerAngles
    {
        get
        {
            return ToEulerAngles(this);
        }
        set
        {
            LQuaternion q = Euler(value);
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;
        }
    }

    public static LQuaternion identity
    {
        get
        {
            return new LQuaternion(0, 0, 0, 1);
        }
    }

    public static LQuaternion zero
    {
        get
        {
            return new LQuaternion(0, 0, 0, 0);
        }
    }

    public static LFloat Angle(LQuaternion a, LQuaternion b)
    {
        LQuaternion q = b * LQuaternion.Inverse(a);
        q.Normalize();
        return LMath.Acos(q.w);
    }

    public static LQuaternion AngleAxis(LFloat angle, LVector3 axis)
    {
        LFloat radian2 = angle * 0.5d * LMath.DegToRad;
        LFloat sina = LMath.Sin(radian2);
        LFloat cosa = LMath.Cos(radian2);
        axis = axis.normalized;
        return new LQuaternion(sina * axis.x, sina * axis.y, sina * axis.z, cosa);
    }

    public static LFloat Dot(LQuaternion a, LQuaternion b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    }

    public static LQuaternion Euler(LVector3 euler)
    {
        LFloat ex = euler.x * LMath.DegToRad / 2;
        LFloat ey = euler.y * LMath.DegToRad / 2;
        LFloat ez = euler.z * LMath.DegToRad / 2;
        LFloat sinex = LMath.Sin(ex);
        LFloat siney = LMath.Sin(ey);
        LFloat sinez = LMath.Sin(ez);
        LFloat cosex = LMath.Cos(ex);
        LFloat cosey = LMath.Cos(ey);
        LFloat cosez = LMath.Cos(ez);
        LQuaternion rotation;
        rotation.x = sinez * cosex * cosey - cosez * sinex * siney;
        rotation.y = cosez * sinex * cosey + sinez * cosex * siney;
        rotation.z = cosez * cosex * siney - sinez * sinex * cosey;
        rotation.w = cosez * cosex * cosey + sinez * sinex * siney;
        rotation.Normalize();
        return rotation;
    }

    public static LVector3 ToEulerAngles(LQuaternion rotation)
    {
        rotation.Normalize();
        return new LVector3(
            LMath.Atan2(2 * (rotation.w * rotation.z + rotation.x * rotation.y), 1 - 2 * (rotation.z * rotation.z + rotation.x * rotation.x)),
            LMath.Asin(2 * (rotation.w * rotation.x - rotation.y * rotation.z)),
            LMath.Atan2(2 * (rotation.w * rotation.y + rotation.z * rotation.x), 1 - 2 * (rotation.x * rotation.x + rotation.y * rotation.y))
        );
    }

    public static LQuaternion FromToRotation(LVector3 fromDirection, LVector3 toDirection)
    {
        LFloat angle = LVector3.Angle(fromDirection, toDirection);
        LVector3 axis = fromDirection.Cross(toDirection);
        return AngleAxis(angle, axis);
    }

    public static LQuaternion Inverse(LQuaternion rotation)
    {
        LQuaternion inv = rotation.conjugation;
        if (rotation.sqrtMagnitude != 1)
        {
            inv *= (1 / rotation.sqrtMagnitude);
        }
        return inv;
    }

    public static LQuaternion LookRotation(LVector3 forward)
    {
        return FromToRotation(LVector3.forward, forward);
    }
    public static LQuaternion LookRotation(LVector3 forward, LVector3 upwards)
    {
        return LookRotation(forward.Cross(upwards).Cross(upwards));
    }

    public static LQuaternion RotateTowards(LQuaternion from, LQuaternion to, LFloat maxDegreesDelta)
    {
        LFloat angle = Angle(from, to);
        return Slerp(from, to, maxDegreesDelta / angle);
    }

    public static LQuaternion Lerp(LQuaternion from, LQuaternion to, LFloat t)
    {
        t = LMath.Clamp(t, 0, 1);
        return (1 - t) * from + t * to;
    }

    public static LQuaternion Slerp(LQuaternion from, LQuaternion to, LFloat t)
    {
        t = LMath.Clamp(t, 0, 1);
        LFloat diff = Angle(from, to) * LMath.DegToRad;
        LFloat sind = LMath.Sin(diff);
        LFloat sintd = LMath.Sin(t * diff);
        LFloat sin1td = LMath.Sin((1 - t) * diff);
        return (sin1td / sind) * from + (sintd / sind) * to;
    }
}
