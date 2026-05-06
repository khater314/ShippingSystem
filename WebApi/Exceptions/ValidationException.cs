namespace WebApi.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("A validation error occurred.") { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
