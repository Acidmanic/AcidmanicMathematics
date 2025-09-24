using Acidmanic.Mathematics.Models;

namespace Acidmanic.Mathematics.Extensions;

public static class PcaResultFeatureMatrixExtensions
{
    /// <summary>
    /// This method will perform the final stage of Principal Component Analysis for Dimension Reduction. It sorts
    /// eigen values in descending order and eigenvectors accordingly, choose the first p eigen values and eigen vectors
    ///  who are assumed to preserve the given portion of entropy (therefore information).  
    /// </summary>
    /// <param name="pca">The Principal Component Analysis calculation result</param>
    /// <param name="informationFraction">A number between 0 and 1 (including).</param>
    /// <returns>Feature Vectors and their associated eigen values and index arrays to
    /// select and select-back from/to original values</returns>
    /// <exception cref="ArgumentException">If given informationFraction is less than 0 or larger than 1</exception>
    public static PcaReductionParameters GetPcaReductionParameters(this PcaResult pca, double informationFraction = 0.95)
    {
        if (informationFraction < 0 || informationFraction > 1)
        {
            throw new ArgumentException("Information fraction must be a number between 0 and 1");
        }

        //var sortedIndexes = pca.EigenValues.Elements.SortedIndexes((d1, d2) => d1 > d2);
        
        var sortedIndexes = pca.EigenValues.Elements.SortedIndexes(CompareEigenValues);

        var eigenSum = MathX.Abs(pca.EigenValues).Sum();
        
        var sortedEigenValues = new Matrix(pca.EigenValues.Elements.Compose(sortedIndexes));
        
        var fractions = sortedEigenValues / eigenSum;

        var accumulated = 0.0d;
        
        var savingIndex = fractions.Elements.IndexOf(d =>
        {
            accumulated += Math.Abs(d);
            
            return accumulated >= informationFraction;
        });

        return GetPcaReductionParameters(pca, sortedIndexes, savingIndex+1);
    }
    
    
    public static PcaReductionParameters GetPcaReductionParameters(this PcaResult pca, int numberOfReducedDimensions)
    {
        
        var sortedIndexes = pca.EigenValues.Elements.SortedIndexes(CompareEigenValues);

        return GetPcaReductionParameters(pca, sortedIndexes, numberOfReducedDimensions);
    }
    
    private static PcaReductionParameters GetPcaReductionParameters(this PcaResult pca,
        int[] sortedIndexes,
        int numberOfReducedDimensions)
    {
        
        var featureVectors = new PcaReductionParameters();

        featureVectors.SortIndexes = sortedIndexes.SubSet(0, numberOfReducedDimensions);

        featureVectors.SortedEigenValues = new Matrix(pca.EigenValues.Elements.Compose(featureVectors.SortIndexes));

        featureVectors.FeatureVectors = pca.EigenVectors.Compose(featureVectors.SortIndexes);

        featureVectors.SortBackIndexes = featureVectors.SortIndexes.SortedIndexes((i1, i2) => i1 <= i2);

        featureVectors.OriginalFeaturesCount = pca.EigenValues.Length;

        featureVectors.ReducedFeaturesCount = featureVectors.SortIndexes.Length;
        
        featureVectors.FeatureMatrix = featureVectors.FeatureVectors.AggregateAsMatrix(Matrix2Dimensions.Columns);

        featureVectors.PreservedEntropyPortion =
            MathX.Abs(pca.EigenValues).Sum()/
            MathX.Abs(featureVectors.SortedEigenValues).Sum();
        
        return featureVectors;
    }

    private static int CompareEigenValues(double eig1, double eig2)
    {
        eig1 = Math.Abs(eig1);
        eig2 = Math.Abs(eig2);

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (eig1 == eig2)
        {
            return 0;
        }
        return eig1>eig2? -1 : 1;
    }
}