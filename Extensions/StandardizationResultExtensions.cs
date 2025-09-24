using Acidmanic.Mathematics.Models;

namespace Acidmanic.Mathematics.Extensions;

public static class StandardizationResultExtensions
{
    public static Matrix TransformToStandardForm
        (this StandardizationResult standardizationResult, Matrix sample)
    {
        sample.CheckIfSameNumberOfElements(standardizationResult.Averages, "Transformation");

        var standard = (sample - standardizationResult.Averages) / standardizationResult.StandardDeviations;

        return standard;
    }

    public static Matrix TransformBackFromStandardForm(
        this StandardizationResult standardizationResult,
        Matrix standard)
    {
        standard.CheckIfSameNumberOfElements(standardizationResult.Averages, "Transformation");

        var sample = standard * standardizationResult.StandardDeviations + standardizationResult.Averages;

        return sample;
    }
}