using Acidmanic.Mathematics.Exceptions;

namespace Acidmanic.Mathematics.Extensions;

/// <summary>
/// This class keeps repeating routines for size and dimension check for other methods
/// </summary>
internal static class MatrixConditionProbingExtensions
{





    public static void CheckIf2D(this Matrix m,string operationName)
    {
        if (m.Size.Length != 2)
        {
            throw new InvalidMatrixDimensionsException($"Matrix must be a 2-dimensional matrix for {operationName}");
        }
    }
    
    public static void CheckIfSameNumberOfElements(this Matrix m, Matrix other ,string operationName)
    {
        if (m.Length != other.Length)
        {
            throw new InvalidMatrixDimensionsException($"Matrices need to have same number of elements for {operationName}");
        }
    }
}