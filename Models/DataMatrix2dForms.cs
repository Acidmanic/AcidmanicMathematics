namespace Acidmanic.Mathematics.Models;

public enum DataMatrix2dForms
{
    RowsAreSamplesColumnsAreFeatures =0,
    ColumnsAreSamplesRowsAreFeatures =1,   
}

public static class DataMatrix2dFormsDimensionExtensions
{


    public static int[] TranslateIndex(this DataMatrix2dForms form, int row, int column)
    {
        if (form == DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures)
        {
            return new[] { row, column };
        }

        return new[] { column, row };
    }
    
    
}