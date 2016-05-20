public struct LFloat
{
    // 1 << 20
    public static long PositiveInfinity = 1048576;
    public static long NegativeInfinity = -PositiveInfinity;

    private static long RatePositiveInfinity = PositiveInfinity * Rate;
    private static long RateNegativeInfinity = NegativeInfinity * Rate;

    public const long Rate = 10000;

    public long value;

    private LFloat(long val)
    {
        value = LMath.Clamp(val, RateNegativeInfinity, RatePositiveInfinity);
    }

    public double ToDouble()
    {
        return value / (double)Rate;
    }

    public float ToFloat()
    {
        return value / (float)Rate;
    }

    public override string ToString()
    {
        return ToDouble().ToString();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return this == (LFloat)obj;
    }

    public static implicit operator LFloat(float val)
    {
        throw new System.NotSupportedException();
    }

    public static implicit operator LFloat(double val)
    {
        return new LFloat((long)(val * Rate));
    }

    public static implicit operator LFloat(int val)
    {
        return new LFloat(val * Rate);
    }

    public static implicit operator LFloat(long val)
    {
        return new LFloat(val * Rate);
    }

    public static implicit operator double(LFloat val)
    {
        return val.ToDouble();
    }

    public static LFloat operator -(LFloat val)
    {
        return new LFloat(-val.value);
    }

    public static LFloat operator +(LFloat val1, LFloat val2)
    {
        return new LFloat(val1.value + val2.value);
    }

    public static LFloat operator -(LFloat val1, LFloat val2)
    {
        return new LFloat(val1.value - val2.value);
    }

    public static LFloat operator *(LFloat val1, LFloat val2)
    {
        return new LFloat(val1.value * val2.value / Rate);
    }

    public static LFloat operator /(LFloat val1, LFloat val2)
    {
        if (val2.value == 0)
        {
            return val1 >= 0 ? PositiveInfinity : NegativeInfinity;
        }
        else
        {
            return new LFloat(val1.value * Rate / val2.value);
        }
    }

    public static bool operator >(LFloat val1, LFloat val2)
    {
        return val1.value > val2.value;
    }

    public static bool operator <(LFloat val1, LFloat val2)
    {
        return val1.value < val2.value;
    }

    public static bool operator ==(LFloat val1, LFloat val2)
    {
        return val1.value == val2.value;
    }

    public static bool operator !=(LFloat val1, LFloat val2)
    {
        return val1.value != val2.value;
    }

    public static bool operator >=(LFloat val1, LFloat val2)
    {
        return val1.value >= val2.value;
    }

    public static bool operator <=(LFloat val1, LFloat val2)
    {
        return val1.value <= val2.value;
    } 
}