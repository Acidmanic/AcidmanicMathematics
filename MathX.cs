using Acidmanic.Mathematics.Exceptions;
using Acidmanic.Mathematics.Extensions;
using Acidmanic.Mathematics.Models;
using MathNet.Numerics.Financial;
using MathNet.Numerics.LinearAlgebra;

namespace Acidmanic.Mathematics;

public static class MathX
{
    public static double EuclideanDistance(Matrix m1, Matrix m2)
    {
        var distance = 0.0;

        if (m1.DataSize != m2.DataSize)
        {
            throw new InvalidMatrixSizeException();
        }

        for (int i = 0; i < m1.DataSize; i++)
        {
            distance += Math.Pow((m1.Elements[i] - m2.Elements[i]), 2);
        }

        distance = Math.Sqrt(distance);

        return distance;
    }

    public static Matrix DotProduct(Matrix a, Matrix b)
    {
        if (a.Size.Length != 2 || b.Size.Length != 2)
        {
            throw new InvalidMatrixDimensionsException("Dot product is only applicable on 2D matrices");
        }

        if (a.Size[1] != b.Size[0])
        {
            throw new InvalidMatrixSizeException("Left operands columns must be equal to right operand rows.");
        }

        var rows = a.Size[0];
        var columns = b.Size[1];
        var commonSize = a.Size[1];

        var result = new Matrix(rows, columns);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var value = 0.0;

                for (int k = 0; k < commonSize; k++)
                {
                    value += a[i, k] * b[k, j];
                }

                result[i, j] = value;
            }
        }

        return result;
    }


    /// <summary>
    /// This method calculates the vector dot-product of matrices elements regardless of matrices shapes. But
    /// The matrices do need to have same number of elements.   
    /// </summary>
    /// <param name="v1">Left Operand</param>
    /// <param name="v2">Right Operand</param>
    /// <returns>The dot product of matrices v1 and v2, where both matrices are treated as vectors.</returns>
    public static double DotProductAsVectors(Matrix v1, Matrix v2)
    {
        v1.CheckIfSameNumberOfElements(v2,"Vector Dot Product Calculation");

        var dotProduct = 0.0;
        
        var size = v1.Length;
        
        for (int i = 0; i < size; i++)
        {
            dotProduct += v1.Elements[i] * v2.Elements[i];
        }

        return dotProduct;
    }

    public static Matrix Project(Matrix vectorA, Matrix vectorB)
    {
        if (vectorA.Length != vectorB.Length)
        {
            throw new InvalidMatrixSizeException("Vector A and Vector B Must have same number of " +
                                                 "elements in order to perform projection.");
        }


        var length = vectorA.Length;

        var a = vectorA.Reshape(new int[] { length, 1 });
        var b = vectorB.Reshape(new int[] { length, 1 });

        var dotProduct = a.Transpose().DotProduct(b)[0, 0];

        var bLength = b.LengthNorm();

        var scale = dotProduct / bLength;

        var projection = scale * b;

        return projection;
    }


    public static Matrix HouseholderReflector(Matrix xVector)
    {
        var x = xVector.AsVector();

        var w = new Matrix(x.Length).Zeros();

        w[0] = x.EuclideanNorm();

        if (x[0] >= 0)
        {
            w[0] = -w[0];
        }

        var v = w - x;

        v = v.Reshape(v.Length, 1);

        var vTv = v.Transpose().DotProduct(v).Elements[0];

        var vvT = v.DotProduct(v.Transpose());

        var projection = vvT / vTv;

        var h = Matrix.Identity(x.Length) - (2 * projection);

        return h;
    }

    public static PcaResult Pca(Matrix data,
        DataMatrix2dForms form = DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures)
    {
        data.CheckIf2D("Performing Pca");

        var pcaResult = new PcaResult();

        pcaResult.SampleCovariance = data.Covariance(form);

        var mnCov = pcaResult.SampleCovariance.ToMathNetMatrix();

        var evd = mnCov.Evd();

        pcaResult.EigenValues = evd.EigenValues.Real().ToAcidmanicMathematicsMatrix();

        var eigenVectors = new Matrix[evd.EigenVectors.RowCount];

        for (int r = 0; r < eigenVectors.Length; r++)
        {
            eigenVectors[r] = new Matrix(evd.EigenVectors.ColumnCount);

            for (int c = 0; c < eigenVectors[r].Length; c++)
            {
                eigenVectors[r][c] = evd.EigenVectors[r, c];
            }
        }

        pcaResult.EigenVectors = eigenVectors;

        return pcaResult;
    }


    public static Matrix Abs(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Abs);
    }

    public static Matrix Acos(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Acos);
    }

    public static Matrix Acosh(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Acosh);
    }

    public static Matrix Asin(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Asin);
    }

    public static Matrix Asinh(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Asinh);
    }

    public static Matrix Atan(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Atan);
    }

    public static Matrix Atanh(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Atanh);
    }

    public static Matrix Cbrt(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Cbrt);
    }

    public static Matrix Ceiling(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Ceiling);
    }

    public static Matrix Cos(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Cos);
    }

    public static Matrix Cosh(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Cosh);
    }

    public static Matrix Exp(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Exp);
    }

    public static Matrix Floor(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Floor);
    }

    public static Matrix Log(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Log);
    }

    public static Matrix Log2(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Log2);
    }

    public static Matrix Log10(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Log10);
    }

    public static Matrix Round(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Round);
    }

    public static Matrix Sin(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Sin);
    }

    public static Matrix Sinh(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Sinh);
    }

    public static Matrix Sqrt(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Sqrt);
    }

    public static Matrix Tan(Matrix matrix)
    {
        return Matrix.PerformElementWise(matrix, Math.Tan);
    }
}