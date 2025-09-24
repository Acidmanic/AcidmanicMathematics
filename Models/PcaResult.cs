namespace Acidmanic.Mathematics.Models;

public class PcaResult
{
    
    public Matrix SampleCovariance { get; set; }
    
    public Matrix EigenValues { get; set; }
    
    public Matrix[] EigenVectors { get; set; }
}