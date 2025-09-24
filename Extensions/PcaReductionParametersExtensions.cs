using Acidmanic.Mathematics.Models;

namespace Acidmanic.Mathematics.Extensions;

public static class PcaReductionParametersExtensions
{
   
    public static Matrix ReduceDataSet(this PcaReductionParameters reduction, Matrix dataset)
    {
        dataset.CheckIf2D("Pca Dimensional Reduction");

        if (dataset.Size[1] != reduction.OriginalFeaturesCount)
        {
            throw new ArgumentException("Given data set can not belong to this reduction parameter set.");
        }

        return reduction.FeatureMatrix.Transpose().DotProduct(dataset.Transpose()).Transpose();
    }
    
    public static Matrix ReduceSample(this PcaReductionParameters reduction, Matrix sampleVector)
    {

        if (sampleVector.Length != reduction.OriginalFeaturesCount)
        {
            throw new ArgumentException("Given sample vector can not belong to this reduction parameter set.");
        }

        return reduction.FeatureMatrix.Transpose().
            DotProduct(sampleVector.Reshape(1,sampleVector.Length).Transpose())
            .Transpose();
    }
}