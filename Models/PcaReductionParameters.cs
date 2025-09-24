using Acidmanic.Mathematics.Extensions;

namespace Acidmanic.Mathematics.Models;

public class PcaReductionParameters
{
    
    /// <summary>
    /// Selected and sorted Eigen vectors regarding the selected and sorted eigen values 
    /// </summary>
    public Matrix[] FeatureVectors { get; set; }
    /// <summary>
    /// Selected and sorted eigen values from the Principal Component Analysis original results.
    /// </summary>
    public Matrix SortedEigenValues { get; set; }
    /// <summary>
    /// The portion of entropy which is assumed to be preserved during dimension reduction 
    /// </summary>
    public double PreservedEntropyPortion { get; set; }
    /// <summary>
    /// Selected eigen vectors and eigen values indexes descendant-ordered by eigen-values. Choosing items from
    /// original eigen-vectors and eigen-values, by this array's order, will give FeatureVectors and SortedEigenValues
    /// </summary>
    public int[] SortIndexes { get; set; }
    /// <summary>
    /// The back transform of SortedIndexes. Choosing items from FeatureVectors and SortedEigenValues by this
    /// array's order, will give the original eigen values and eigen vectors, except those omitted in reduction.  
    /// </summary>
    public int[] SortBackIndexes { get; set; }

    public int OriginalFeaturesCount { get; set; }
    
    public int ReducedFeaturesCount { get; set; }
    
    
    /// <summary>
    /// This is a column aggregation of FeatureVectors to be used for dimension reduction over whole data set.
    /// This method assumes your data matrix has samples on it's rows  
    /// </summary>
    public Matrix FeatureMatrix { get; set; }
}