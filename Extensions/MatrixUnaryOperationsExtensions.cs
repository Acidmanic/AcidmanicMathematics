namespace Acidmanic.Mathematics.Extensions;

public static class MatrixUnaryOperationsExtensions
{
    public static double Product(this Matrix m)
    {
        return m.Elements.Product();
    }

    public static double Sum(this Matrix m)
    {
        return m.Elements.Sum();
    }

    public static double Max(this Matrix m)
    {
        return m.Elements.Max();
    }

    public static double Min(this Matrix m)
    {
        return m.Elements.Min();
    }

    public static double Average(this Matrix m)
    {
        return m.Elements.Average();
    }

    public static double SampleVariance(this Matrix m)
    {
        return m.Elements.SampleVariance();
    }

    public static double StandardDeviation(this Matrix m)
    {
        return m.Elements.StandardDeviation();
    }
    
}