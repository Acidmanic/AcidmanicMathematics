
namespace Acidmanic.Mathematics.Extensions;

public static class MatrixMathNetConversionExtensions
{



    public static MathNet.Numerics.LinearAlgebra.Matrix<double> ToMathNetMatrix
        (this Matrix matrix)
    {
        matrix.CheckIf2D("Conversion to MathNet Matrices");

        var mnMatrix = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build
            .Dense(matrix.Size[0], matrix.Size[1],
                (r, c) => matrix[r, c]);

        return mnMatrix;
    }



    public static Matrix ToAcidmanicMathematicsMatrix
        (this MathNet.Numerics.LinearAlgebra.Matrix<double> mnMatrix)
    {

        var matrix = new Matrix(mnMatrix.RowCount, mnMatrix.ColumnCount);

        for (int r = 0; r < mnMatrix.RowCount; r++)
        {
            for (int c = 0; c < mnMatrix.ColumnCount; c++)
            {
                matrix[r, c] = mnMatrix[r, c];
            }
        }

        return matrix;
    }

    public static Matrix ToAcidmanicMathematicsMatrix
        (this MathNet.Numerics.LinearAlgebra.Vector<double> vector)
    {
        var matrix = new Matrix(vector.Count);

        for (int i = 0; i < vector.Count; i++)
        {
            matrix.Elements[i] = vector[i];
        }

        return matrix;
    }
    
    
    public static MathNet.Numerics.LinearAlgebra.Vector<double> ToMathNetVector
        (this Matrix matrix)
    {
        var vector = MathNet.Numerics.LinearAlgebra.Vector<double>.Build
            .Dense(matrix.Length,index => matrix.Elements[index]);

        return vector;
    }
}