using Acidmanic.Mathematics.Exceptions;

namespace Acidmanic.Mathematics;

public partial class Matrix
{
    public readonly double[] Elements;

    private readonly IndexMap _indexMap;
    
    public int Length => Elements.Length;

    public int Dimensions => _indexMap.Dimensions;

    public int[] Size => _indexMap.Size;

    public int DataSize => _indexMap.DataSize;
    
    private Matrix(double[] elements,IndexMap map)
    {
        Elements = elements;
        _indexMap = map;
    }

    public Matrix(params int[] sizes)
    {
        _indexMap = new IndexMap(sizes);
        Elements = new double[_indexMap.DataSize];
    }

    public double this[params int[] indexes]
    {
        get => Elements[_indexMap.Map(indexes)];
        set => Elements[_indexMap.Map(indexes)] = value;
    }

    public Matrix Reshape(params int[] size)
    {
        var map = new IndexMap(size);

        if (map.DataSize != _indexMap.DataSize)
        {
            throw new InvalidMatrixSizeException("A Matrix can only be re-shaped into dimension sets" +
                                                 " with similar total number of elements.");
        }

        var dataClone = new double[Elements.Length];

        Array.Copy(Elements, dataClone, Elements.Length);

        return new Matrix(dataClone, map);
    }
}