using System.Runtime.Serialization;

namespace PizzaHub.Exceptions.Exceptions;

[Serializable]
public class ClientException : BaseException
{
    public ClientException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected ClientException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public List<string>? ErrorMessages { get; set; }
}