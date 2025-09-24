namespace Acidmanic.Mathematics.Extensions;

public static class MatrixNormExtensions
{



    public static double OneNorm(this Matrix m)
    {
        
        m.CheckIf2D("1-Norm calculation");

        var sum = m.Sum(0);

        return sum.Max();
    }
    
    public static double InfinityNorm(this Matrix m)
    {
        
        m.CheckIf2D("Infinity-Norm calculation");

        var sum = m.Sum(1);

        return sum.Max();
    }
    
    public static double EuclideanNorm(this Matrix m)
    {
        m *= m;
        
        var sum = m.Sum();

        return Math.Sqrt(sum);
    }

    public static double FrobeniusNorm(this Matrix m)
    {

        var trace = m.Transpose().DotProduct(m).Trace();

        var frobenius = Math.Sqrt(trace);

        return frobenius;
    }
    
    public static double MaxNorm(this Matrix m)
    {
        return m.Max();
    }

    /// <summary>
    ///  EuclideanNorm^2
    /// </summary>
    public static double LengthNorm(this Matrix m)
    {
        return (m * m).Sum();
    }
    
}