using Acidmanic.Mathematics.Extensions;

namespace Acidmanic.Mathematics;

public partial class Matrix
{
    public static Matrix Identity(int size, int numberOfDimensions = 2)
    {
        var matrixSize = new int[numberOfDimensions]
            .Select(v => size).ToArray();

        var identityMatrix = new Matrix(matrixSize).Zeros();

        for (int i = 0; i < size; i++)
        {
            var diagonalIndex = new int[numberOfDimensions].Select(v => i).ToArray();

            identityMatrix[diagonalIndex] = 1.0;
        }

        return identityMatrix;
    }



    public static Matrix GivensRotation(int size, int p, int q,double phi)
    {
        var rotation = Identity(size);

        var s = Math.Sign(phi);

        var c = Math.Cos(phi);

        rotation[p, p] = c;
        rotation[p, q] = s;
        rotation[q, p] = -s;
        rotation[q, q] = c;

        return rotation;
    }


    public static Matrix Random(params int[] size)
    {
        return new Matrix(size).Randomize();
    }
}