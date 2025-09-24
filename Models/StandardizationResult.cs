using System.Net;

namespace Acidmanic.Mathematics.Models;

public class StandardizationResult
{
    public Matrix Standardized { get; set; }
    
    public Matrix StandardDeviations { get; set; }
    
    public Matrix Averages { get; set; }


    public static implicit operator Matrix(StandardizationResult result)
    {
        return result.Standardized;
    }
    
}