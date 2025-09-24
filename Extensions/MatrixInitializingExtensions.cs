namespace Acidmanic.Mathematics.Extensions;

public static class MatrixInitializingExtensions
{
    private static readonly Random R = new Random();


    public static Matrix Randomize(this Matrix m)
    {
        Matrix.ManipulateElementWise(m, d => R.NextDouble());

        return m;
    }
    
    public static Matrix Ones(this Matrix m)
    {
        Matrix.ManipulateElementWise(m, d => 1);

        return m;
    }
    
    public static Matrix Zeros(this Matrix m)
    {
        Matrix.ManipulateElementWise(m, d => 0);

        return m;
    }
    
    public static Matrix Set(this Matrix m, double value)
    {
        Matrix.ManipulateElementWise(m, d => value);

        return m;
    }
}