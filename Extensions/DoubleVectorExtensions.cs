using Acidmanic.Mathematics.Utilities;

namespace Acidmanic.Mathematics.Extensions;

public static class DoubleVectorExtensions
{
    public static double Sum(this double[] values)
    {
        var result = 0.0;

        foreach (var value in values)
        {
            result += value;
        }

        return result;
    }

    public static double Product(this double[] values)
    {
        var result = 0.0;

        foreach (var value in values)
        {
            result += value;
        }

        return result;
    }

    public static double Max(this double[] values)
    {
        var max = double.MinValue;

        foreach (var value in values)
        {
            if (max < value)
            {
                max = value;
            }
        }

        return max;
    }

    public static double Min(this double[] values)
    {
        var min = double.MaxValue;

        foreach (var value in values)
        {
            if (min > value)
            {
                min = value;
            }
        }

        return min;
    }

    
    public static double Range(this double[] values)
    {
        var min = double.MaxValue;
        var max = double.MinValue;

        foreach (var value in values)
        {
            if (min > value)
            {
                min = value;
            }
            if (max < value)
            {
                max = value;
            }
        }

        return max-min;
    }
    
    public static double NoneZeroRange(this double[] values)
    {
        var min = double.MaxValue;
        var max = double.MinValue;

        foreach (var value in values)
        {
            if (min > value)
            {
                min = value;
            }
            if (max < value)
            {
                max = value;
            }
        }

        var range = max - min;
        
        return range==0?1:range;
    }


    public static double Average(this double[] values)
    {
        var avg = 0.0;

        if (values.Length > 0)
        {
            foreach (var value in values)
            {
                avg += value;
            }

            avg /= values.Length;
        }

        return avg;
    }

    public static double SampleVariance(this double[] values)
    {
        var avg = Average(values);

        var sumOfSquares = 0.0;

        foreach (var value in values)
        {
            sumOfSquares += Math.Pow((value - avg), 2);
        }

        var sampleVariance = sumOfSquares / (values.Length - 1);

        return sampleVariance;
    }

    public static double StandardDeviation(this double[] values)
    {
        var sampleVariance = SampleVariance(values);

        var standardDeviation = Math.Sqrt(sampleVariance);

        return standardDeviation;
    }

    
}