using System.Runtime.Serialization;

namespace Battleships.Common.Exceptions;

public class UserInputException : Exception
{
    public UserInputException()
    {
    }

    protected UserInputException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserInputException(string? message) : base(message)
    {
    }

    public UserInputException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}