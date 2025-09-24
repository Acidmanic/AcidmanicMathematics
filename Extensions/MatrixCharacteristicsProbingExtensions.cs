using Acidmanic.Mathematics.Utilities;

namespace Acidmanic.Mathematics.Extensions;

public static class MatrixCharacteristicsProbingExtensions
{
    public static bool IsVector(this Matrix m)
    {
        return m.Size.Length == 1;
    }


    public static bool IsHyperCube(this Matrix m)
    {
        if (m.Size.Length != 0)
        {
            var theSize = m.Size[0];

            for (int i = 1; i < m.Size.Length; i++)
            {
                if (m.Size[i] != theSize)
                {
                    return false;
                }
            }
        }

        return true;
    }


    public static bool IsSymmetric(this Matrix m, double precision = 0.00000000000000000000000000000001)
    {
        if (!m.IsHyperCube())
        {
            return false;
        }
        
        var indexes = MatrixDimensions.PossibleIndexes(m.Size);

        foreach (var index in indexes)
        {
            var reverse = index.Reverse().ToArray();

            if (Math.Abs(m[index] - m[reverse]) > precision)
            {
                return false;
            }
        }

        return true;
    }
}