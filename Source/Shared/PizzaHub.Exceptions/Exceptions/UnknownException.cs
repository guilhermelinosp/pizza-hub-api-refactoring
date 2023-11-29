using System.Runtime.Serialization;

namespace PizzaHub.Exceptions.Exceptions;

[Serializable]
public class UnknownException : BaseException
{
    public UnknownException(string message) : base(message)
    {
    }

    protected UnknownException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}