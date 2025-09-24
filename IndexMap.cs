namespace Acidmanic.Mathematics;

public class IndexMap
{

    private readonly int[] _factorials;
    public int Dimensions { get; }
    
    public int[] Size { get; }

    public int DataSize { get; }
    
    public IndexMap(int[] size)
    {
        Dimensions = size.Length;

        _factorials = new int[size.Length];
        
        var factorial = 1;

        for (int i = Dimensions - 1; i >= 0; i--)
        {
            var index = Dimensions - i - 1;
            
            _factorials[index] = factorial;
            
            factorial *= size[index];
        }

        DataSize = factorial;
        
        Size = size;
    }


    public int Map(params int[] indexes)
    {
        var index = 0;

        for (int i = 0; i < Dimensions; i++)
        {
            index += indexes[i] * _factorials[i];
        }

        return index;
    }
    
    
    
    
    
}