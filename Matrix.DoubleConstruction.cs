namespace Acidmanic.Mathematics;

public partial class Matrix
{
    public Matrix(double[] values) : this(values.Length)
    {
        Elements = values;
    }

    public Matrix(double[,] values) : this(values.GetLength(0), values.GetLength(1))
    {
        for (int i = 0; i < Size[0]; i++)
        {
            for (int j = 0; j < Size[1]; j++)
            {
                var index = _indexMap.Map(i, j);

                Elements[index] = values[i, j];
            }
        }
    }

    public Matrix(double[,,] values) : this(
        values.GetLength(0),
        values.GetLength(1),
        values.GetLength(2))
    {
        for (int i = 0; i < Size[0]; i++)
        {
            for (int j = 0; j < Size[1]; j++)
            {
                for (int k = 0; k < Size[3]; k++)
                {
                    var index = _indexMap.Map(i, j, k);

                    Elements[index] = values[i, j, k];
                }
            }
        }
    }
}