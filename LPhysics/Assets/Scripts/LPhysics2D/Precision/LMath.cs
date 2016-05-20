using System;

public sealed class LMath
{
    public const LFloat PI = 3.14159d;

    public const LFloat DegToRad = PI / 180;

    public const LFloat RadToDeg = 180 / PI;

    public static long Clamp(long value, long min, long max)
    {
        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        return (long)value;
    }

    public static LFloat Clamp(LFloat value, LFloat min, LFloat max)
    {
        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        return value;
    }

    public static LFloat Sqrt(LFloat value)
    {
        if (value.value < 0)
        {
            throw new NotSupportedException();
        }
        else if (value.value == 0)
        {
            return 0;
        }
        else
        {
            return Math.Sqrt(value);
        }
    }

    public static LFloat Abs(LFloat value)
    {
        if (value.value >= 0)
        {
            return value;
        }
        else
        {
            value.value *= -1;
            return value;
        }
    }

    public static LFloat Sign(LFloat value)
    {
        return value >= 0 ? 1 : -1;
    }

    public static LFloat Sin(LFloat value)
    {
        return Math.Sin(value);
    }

    public static LFloat Cos(LFloat value)
    {
        return Math.Cos(value);
    }

    public static LFloat Tan(LFloat value)
    {
        return Math.Tan(value);
    }

    public static LFloat Atan(LFloat value)
    {
        return Math.Atan(value);
    }

    public static LFloat Atan2(LFloat y, LFloat x)
    {
        return Math.Atan2(y, x);
    }

    public static LFloat Asin(LFloat value)
    {
        return Math.Asin(value);
    }

    public static LFloat Acos(LFloat value)
    {
        return Math.Acos(value);
    }

}
