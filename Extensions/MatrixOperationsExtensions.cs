using Acidmanic.Mathematics.Exceptions;
using Acidmanic.Mathematics.Models;
using Acidmanic.Mathematics.Utilities;

namespace Acidmanic.Mathematics.Extensions;

public static class MatrixOperationsExtensions
{
    public static Matrix DotProduct(this Matrix a, Matrix b) => MathX.DotProduct(a, b);

    public static double DotProductAsVectors(this Matrix m, Matrix other) => MathX.DotProductAsVectors(m, other);


    public static Matrix Transpose(this Matrix m, int dimension1, int dimension2)
    {
        var size = MatrixDimensions.SwapDimensions(m.Size, dimension1, dimension2);

        var result = new Matrix(size);

        var sourceIndexes = MatrixDimensions.PossibleIndexes(m.Size);

        foreach (var srcIndex in sourceIndexes)
        {
            var dstIndex = MatrixDimensions.SwapDimensions(srcIndex, dimension1, dimension2);

            var elementValue = m[srcIndex];

            result[dstIndex] = elementValue;
        }

        return result;
    }

    public static Matrix Transpose(this Matrix m)
    {
        var size = m.Size.Reverse().ToArray();

        var result = new Matrix(size);

        var sourceIndexes = MatrixDimensions.PossibleIndexes(m.Size);

        foreach (var srcIndex in sourceIndexes)
        {
            var dstIndex = srcIndex.Reverse().ToArray();

            var elementValue = m[srcIndex];

            result[dstIndex] = elementValue;
        }

        return result;
    }


    public static double Trace(this Matrix m)
    {
        if (!m.IsHyperCube())
        {
            throw new InvalidMatrixDimensionsException("Trace can be defined only for matrices which " +
                                                       "have same length in every dimension (HyperCubes)");
        }

        var trace = 0.0;

        if (m.Size.Length > 0)
        {
            var theSize = m.Size[0];

            for (int i = 0; i < theSize; i++)
            {
                var index = m.Size.Select(_ => i).ToArray();

                trace += m[index];
            }
        }

        return trace;
    }


    public static Matrix Covariance(this Matrix data,
        DataMatrix2dForms form = DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures)
    {
        if (form == DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures)
        {
            return CovarianceOverRows(data);
        }

        return CovarianceOverColumns(data);
    }

    public static Matrix CovarianceOverRows(this Matrix data)
    {
        if (data.Size.Length != 2)
        {
            throw new InvalidMatrixDimensionsException("Covariance matrix can be defined only for 2D matrices");
        }

        var means = data.Average(0);

        var expandedMeans = new Matrix(data.Size[0], 1).Ones().DotProduct(means);

        var centeredData = data - expandedMeans;

        var samplesCount = data.Size[0];

        var sampleCovariance = centeredData.Transpose().DotProduct(centeredData) / (samplesCount - 1.0);

        return sampleCovariance;
    }

    public static Matrix CovarianceOverColumns(this Matrix data)
    {
        if (data.Size.Length != 2)
        {
            throw new InvalidMatrixDimensionsException("Covariance matrix can be defined only for 2D matrices");
        }

        var means = data.Average(1);

        var expandedMeans = means.DotProduct(new Matrix(1, data.Size[1]).Ones());

        var centeredData = data - expandedMeans;

        var samplesCount = data.Size[1];

        var sampleCovariance = centeredData.Transpose().DotProduct(centeredData) / (samplesCount - 1.0);

        return sampleCovariance;
    }


    public static Matrix AsVector(this Matrix m)
    {
        return new Matrix(m.Elements);
    }

    public static QrDecompositionResult QrDecompose(this Matrix m)
    {
        m.CheckIf2D("Q R Decomposition");

        var columns = m.Size[1];

        var rows = m.Size[0];

        var vectors = m.Vectorize(1);

        var orthogonalSet = new Matrix[columns];

        for (int i = 0; i < columns; i++)
        {
            var direction = vectors[i];

            for (int prev = 0; prev < i; prev++)
            {
                direction -= MathX.Project(vectors[i], orthogonalSet[prev]);
            }

            var directionNorm = direction.EuclideanNorm();

            orthogonalSet[i] = direction / directionNorm;
        }

        var q = new Matrix(rows, columns);

        for (int i = 0; i < columns; i++)
        {
            var columnLength = orthogonalSet[i].EuclideanNorm();

            for (int j = 0; j < rows; j++)
            {
                q[j, i] = orthogonalSet[i].Elements[j] / columnLength;
            }
        }

        var r = q.Transpose().DotProduct(m);

        return new QrDecompositionResult
        {
            Q = q,
            R = r
        };
    }


    public static Matrix Diagonal(this Matrix m)
    {
        var length = m.Size.Min();

        var diagonal = new Matrix(length);

        for (int i = 0; i < length; i++)
        {
            var index = m.Size.Select(item => i).ToArray();

            diagonal[i] = m[index];
        }

        return diagonal;
    }
}