using Acidmanic.Mathematics.Models;

namespace Acidmanic.Mathematics.Extensions;

public static class MatrixVectorArrayExtensions
{



    public static Matrix AggregateAsMatrix(this Matrix[] vectors, Matrix2Dimensions vectorsAre = Matrix2Dimensions.Columns)
    {

        // if (vectors.Length == 0)
        // {
        //     return new Matrix(0, 0);
        // }
        //
        // var vectorsLength = vectors[0].Length;
        //
        // foreach (var vector in vectors)
        // {
        //     if (vector is null || vector.Length != vectorsLength)
        //     {
        //         throw new ArgumentException("All vectors must be the same size");
        //     }
        // }
        //
        // var numberOfVectors = vectors.Length;
        //
        // var dstColumns = vectorsAre == Matrix2Dimensions.Columns ? numberOfVectors:vectorsLength;
        // var dstRows = vectorsAre == Matrix2Dimensions.Rows ? numberOfVectors:vectorsLength;
        //
        // Func<int, int, double> read = vectorsAre == Matrix2Dimensions.Columns
        //     ? (r, c) => vectors[c].Elements[r]
        //     : (r, c) => vectors[r].Elements[c];
        //
        // var composedMatrix = new Matrix(dstRows, dstColumns);
        //
        //
        // for (int r = 0; r < dstRows; r++)
        // {
        //     for (int c = 0; c < dstColumns; c++)
        //     {
        //         composedMatrix[r, c] = read(r, c);
        //     }
        // }
        //
        // return composedMatrix;

        return AggregateAsMatrixDynamic(vectors,vectors.Length, vectorsAre);
    }
    
    public static Matrix AggregateAsMatrix(this List<Matrix> vectors, Matrix2Dimensions vectorsAre = Matrix2Dimensions.Columns)
    {
        return AggregateAsMatrixDynamic(vectors,vectors.Count, vectorsAre);
    }
    
    private static Matrix AggregateAsMatrixDynamic(dynamic indexibleVectorsSet,
        int numberOfVectors,
        Matrix2Dimensions vectorsAre = Matrix2Dimensions.Columns)
    {

        if (numberOfVectors == 0)
        {
            return new Matrix(0, 0);
        }

        var vectorsLength = indexibleVectorsSet[0].Length;

        foreach (var vector in indexibleVectorsSet)
        {
            if (vector is null || vector.Length != vectorsLength)
            {
                throw new ArgumentException("All vectors must be the same size");
            }
        }
        
        var dstColumns = vectorsAre == Matrix2Dimensions.Columns ? numberOfVectors:vectorsLength;
        var dstRows = vectorsAre == Matrix2Dimensions.Rows ? numberOfVectors:vectorsLength;

        Func<int, int, double> read = vectorsAre == Matrix2Dimensions.Columns
            ? (r, c) => indexibleVectorsSet[c].Elements[r]
            : (r, c) => indexibleVectorsSet[r].Elements[c];

        var composedMatrix = new Matrix(dstRows, dstColumns);


        for (int r = 0; r < dstRows; r++)
        {
            for (int c = 0; c < dstColumns; c++)
            {
                composedMatrix[r, c] = read(r, c);
            }
        }

        return composedMatrix;
    }
    /// <summary>
    /// Will Create a vector for each h-plain in given dimension. For example if you have a 2x3 matrix,
    /// vectorizing over dimension 0 will give you two 3-elemented vectors, each corresponding to one row
    ///  of original matrix. 
    /// </summary>
    /// <param name="m">The Original Matrix</param>
    /// <param name="dimension">The dimension to iterate over.</param>
    /// <returns>An array of vectors corresponding to h-planes on the given dimension</returns>
    public static Matrix[] Vectorize(this Matrix m, int dimension)
    {
        m.CheckIf2D("Vectorization");

        var vectors = new Matrix[m.Size[dimension]];

        for (int i = 0; i < m.Size[dimension]; i++)
        {
            vectors[i] = m.Project(dimension, values => values[i]);
        }

        return vectors;
    }

    public static Matrix ReshapeToDiagonal(this Matrix vector)
    {
        var size = vector.Length;

        var diagonal = new Matrix(size, size).Set(0);

        for (int i = 0; i < size; i++)
        {
            diagonal[i, i] = vector[i];
        }

        return diagonal;
    }
}