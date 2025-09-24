using Acidmanic.Mathematics.Utilities;

namespace Acidmanic.Mathematics.Extensions;

public static class MatrixProjectionOperationsExtensions
{
    private static Matrix CreateProjectedMatrix(Matrix original, int dimension)
    {
        var resultSize = new int[original.Size.Length];

        for (int i = 0; i < original.Size.Length; i++)
        {
            resultSize[i] = original.Size[i];
        }

        resultSize[dimension] = 1;

        return new Matrix(resultSize);
    }

    public static Matrix Project(this Matrix m, int dimension, Func<double[], double> projection)
    {
        var projected = CreateProjectedMatrix(m, dimension);

        var indexSet = MatrixDimensions.PossibleIndexes(projected.Size);

        var dimensionLength = m.Size[dimension];

        foreach (var index in indexSet)
        {
            var values = new double[dimensionLength];

            for (int i = 0; i < dimensionLength; i++)
            {
                var dimensionedIndex = MatrixDimensions.SetDimensionIndex(index, dimension, i);

                values[i] = m[dimensionedIndex];
            }

            var projectedValue = projection(values);

            projected[index] = projectedValue;
        }

        return projected;
    }


    public static Matrix Sum(this Matrix m, int overDimension)
    {
        return Project(m, overDimension, DoubleVectorExtensions.Sum);
    }

    public static Matrix Product(this Matrix m, int overDimension)
    {
        return Project(m, overDimension, DoubleVectorExtensions.Product);
    }

    public static Matrix Max(this Matrix m, int overDimension)
    {
        return Project(m, overDimension, DoubleVectorExtensions.Max);
    }

    public static Matrix Min(this Matrix m, int overDimension)
    {
        return Project(m, overDimension, DoubleVectorExtensions.Min);
    }
    
    public static Matrix Range(this Matrix m, int overDimension)
    {
        return Project(m, overDimension, DoubleVectorExtensions.Range);
    }
    
    public static Matrix NoneZeroRange(this Matrix m, int overDimension)
    {
        return Project(m, overDimension, DoubleVectorExtensions.NoneZeroRange);
    }

    public static Matrix Average(this Matrix m, int overDimension)
    {
        return Project(m, overDimension, DoubleVectorExtensions.Average);
    }
    
    public static Matrix SampleVariance(this Matrix m, int overDimension)
    {
        return Project(m, overDimension, DoubleVectorExtensions.SampleVariance);
    }
    
    public static Matrix StandardDeviation(this Matrix m, int overDimension)
    {
        return Project(m, overDimension, DoubleVectorExtensions.StandardDeviation);
    }
    
}