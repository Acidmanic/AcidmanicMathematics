using System.Diagnostics;
using Acidmanic.Mathematics.Models;
using Acidmanic.Mathematics.Utilities;

namespace Acidmanic.Mathematics.Extensions;

public static class DataMatrixOperationsExtension
{




    public static StandardizationResult Standardize(this Matrix matrix,
        DataMatrix2dForms form = DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures)
    {
        
        matrix.CheckIf2D("Standardization");

        var dimension = form == DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures ? 0 : 1;

        Func<Matrix, Matrix> expand = form == DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures
            ? m => new Matrix(matrix.Size[0], 1).Ones().DotProduct(m)
            : m => m.DotProduct(new Matrix(1, matrix.Size[1]).Ones());

        var result = new StandardizationResult();

        result.Averages = matrix.Average(dimension);
        
        var mean = expand(result.Averages);

        result.StandardDeviations = matrix.StandardDeviation(dimension);

        var standardDeviation = expand(result.StandardDeviations);

        var standard = (matrix - mean) / standardDeviation;

        result.Standardized = standard;
        
        return result;
    }
    
    /// <summary>
    /// This method will standardize the rows or columns of a matrix, and removes those which
    /// have zero standard deviation since they are not introducing any information
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    public static Matrix StandardizeAndPrune(this Matrix matrix,
        DataMatrix2dForms form = DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures)
    {
        
        matrix.CheckIf2D("Standardization");

        
        var samplesDimension = form == DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures ? 0 : 1;
        var featuresDimension = form == DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures ? 1 : 0;
        
        var standardDeviationVector = matrix.StandardDeviation(samplesDimension);
        
        var indexes = standardDeviationVector.Elements.PickIndexes(d => d != 0);
        
        var featuresAre = form == DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures
            ? Matrix2Dimensions.Columns
            : Matrix2Dimensions.Rows;

        Matrix CherryPick(Matrix m) => m.Vectorize(featuresDimension).Compose(indexes).AggregateAsMatrix(featuresAre);

        standardDeviationVector = CherryPick(standardDeviationVector);

        matrix = CherryPick(matrix);
       

        Func<Matrix, Matrix> expand = form == DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures
            ? m => new Matrix(matrix.Size[0], 1).Ones().DotProduct(m)
            : m => m.DotProduct(new Matrix(1, matrix.Size[1]).Ones());
        
        var standardDeviation = expand(standardDeviationVector);

        var mean = expand(matrix.Average(samplesDimension));

        var standard = (matrix - mean) / standardDeviation;

        return standard;
    }
    
    public static Matrix Normalize(this Matrix matrix,
        DataMatrix2dForms form = DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures)
    {
        
        matrix.CheckIf2D("Normalization");

        var dimension = form == DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures ? 0 : 1;

        Func<Matrix, Matrix> expand = form == DataMatrix2dForms.RowsAreSamplesColumnsAreFeatures
            ? m => new Matrix(matrix.Size[0], 1).Ones().DotProduct(m)
            : m => m.DotProduct(new Matrix(1, matrix.Size[1]).Ones());

        var mean = expand(matrix.Average(dimension));

        var standardDeviation = expand(matrix.NoneZeroRange(dimension));

        var standard = (matrix - mean) / standardDeviation;

        return standard;
    }


    public static Matrix ReadVectorOf2dMatrix(this Matrix m, Matrix2Dimensions vectorDimension, int indexInDimension)
    {
        m.CheckIf2D("Two Dimensional Operation");

        int targetDimension = vectorDimension == Matrix2Dimensions.Rows ? 0 : 1;
        
        int iteratingDimension = vectorDimension == Matrix2Dimensions.Rows ? 1 : 0;

        if (indexInDimension < 0 || indexInDimension > m.Size[targetDimension])
        {
            throw new IndexOutOfRangeException("Index in dimension must be greater than or equal to  0" +
                                               " and smaller than the dimensions size");
        }

        Func<int, double> read = vectorDimension == Matrix2Dimensions.Rows
            ? i => m[indexInDimension, i]
            : i => m[i, indexInDimension];

        var size = m.Size[iteratingDimension];

        var elements = new double[size];
        
        for (int i = 0; i < size; i++)
        {
            elements[i] = read(i);
        }

        return new Matrix(elements);
    }
}