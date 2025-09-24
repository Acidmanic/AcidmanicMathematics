namespace Acidmanic.Mathematics.Extensions;

public static class MatrixHessenbergFormExtensions
{



    private static Matrix ReadColumn(Matrix m, int fromRow, int fromColumn, int length =-1)
    {
        if (length < 0)
        {
            length = m.Size[0] - fromRow;
        }

        var column = new Matrix(length);

        var toRowBoundary = fromRow + length;
        
        for (int i = fromRow; i < toRowBoundary; i++)
        {
            column[i - fromRow] = m[i, fromColumn];
        }

        return column;
    }


    // private static Matrix LeadPad(Matrix m,int padding)
    // {
    //     var padded = new Matrix(m.Size.Select(s => s + padding).ToArray());
    //
    //     for (int i = 0; i < padding; i++)
    //     {
    //         var index = new int[m.Size.Length].Select(s => i).ToArray();
    //
    //         padded[index] = 1;
    //     }
    //
    //     for (int i = padding; i <  i++)
    //     {
    //         
    //     }
    // }
    
    private static Matrix LeadPad(Matrix m,int padding)
    {

        var size = m.Size[0];

        var pSize = size + padding;
        
        var padded = new Matrix(pSize,pSize);

        for (int i = 0; i < padding; i++)
        {
            padded[i,i] = 1;
        }

        for (int i = padding; i < pSize;  i++)
        {
            for (int j = padding; j < pSize; j++)
            {
                padded[i, j] = m[i - padding, j - padding];
            }
        }

        return padded;
    }
    public static Matrix ToHessenberg(this Matrix m)
    {
        m.CheckIf2D("Hessenberg Re-Formatting");

        var columns = m.Size[1];

        var dataMatrix = m;
        
        // -2 is because (by hessenberg definition) we want to zero out the elements below first sub-diagonal
        for (int i = 0; i < columns-2; i++)
        {
            var hhX = ReadColumn(dataMatrix, i + 1, i);

            var hHat = MathX.HouseholderReflector(hhX);
            
            var h = LeadPad(hHat,i+1);

            dataMatrix = h.DotProduct(dataMatrix).DotProduct(h);
        }

        return dataMatrix;
    }
}