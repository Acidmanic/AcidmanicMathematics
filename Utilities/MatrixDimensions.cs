using MathNet.Numerics.Distributions;

namespace Acidmanic.Mathematics.Utilities;

public static class MatrixDimensions
{
    private static List<int[]> PossibleIndexes(int[] size, int dimension)
    {
        var result = new List<int[]>();

        if (dimension >= size.Length - 1)
        {
            for (int i = 0; i < size[dimension]; i++)
            {
                var index = new int[size.Length];

                index[size.Length - 1] = i;

                result.Add(index);
            }
        }
        else
        {
            for (int i = 0; i < size[dimension]; i++)
            {
                var children = PossibleIndexes(size, dimension + 1);

                foreach (var child in children)
                {
                    child[dimension] = i;

                    result.Add(child);
                }
            }
        }

        return result;
    }


    public static List<int[]> PossibleIndexes(params int[] size)
    {
        return PossibleIndexes(size, 0);
    }

    public static int[] SetDimensionIndex(int[] index, int dimension, int indexValue)
    {
        var updatedIndex = new int[index.Length];

        Array.Copy(index, updatedIndex, index.Length);

        updatedIndex[dimension] = indexValue;

        return updatedIndex;
    }
    
    public static int[] SwapDimensions(int[] index, int dimension1, int dimension2)
    {
        var updatedIndex = new int[index.Length];

        Array.Copy(index, updatedIndex, index.Length);

        (updatedIndex[dimension1], updatedIndex[dimension2]) = 
            (updatedIndex[dimension2], updatedIndex[dimension1]);

        return updatedIndex;
    }


    public static int[] FillVectorIndexes(this int[] indexes)
    {
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = i;
        }

        return indexes;
    }

    public static int[] VectorIndexArray(int size)
    {
        return new int[size].FillVectorIndexes();
    }
}