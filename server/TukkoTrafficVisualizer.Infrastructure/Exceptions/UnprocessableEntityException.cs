namespace TukkoTrafficVisualizer.Infrastructure.Exceptions
{
    public class UnprocessableEntityException : Exception
    {
        public UnprocessableEntityException(string? message) : base(message) { }
    }
}
