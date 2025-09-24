using System.Runtime.Serialization;

namespace Acidmanic.Mathematics.Exceptions;

public class InvalidMatrixSizeException:Exception
{
    public InvalidMatrixSizeException()
    {
    }

    protected InvalidMatrixSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidMatrixSizeException(string? message) : base(message)
    {
    }

    public InvalidMatrixSizeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}