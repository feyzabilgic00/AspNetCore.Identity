using System.Runtime.Serialization;

namespace AspNetCore.Identity.Exceptions;

public class NotFoundUserException : Exception
{
    public NotFoundUserException() : base("Kullanıcı adı veya şifre yanlış")
    {
    }

    public NotFoundUserException(string? message) : base(message)
    {
    }

    public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NotFoundUserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
