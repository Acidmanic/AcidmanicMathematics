using System.Runtime.Serialization;

namespace Acidmanic.Mathematics.Exceptions;

public class InvalidMatrixDimensionsException:Exception
{
    public InvalidMatrixDimensionsException()
    {
    }

    protected InvalidMatrixDimensionsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidMatrixDimensionsException(string? message) : base(message)
    {
    }

    public InvalidMatrixDimensionsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}