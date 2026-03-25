namespace ConsoleApp2.Exceptions;

public class RentalLimitExceededException : Exception
{
    public RentalLimitExceededException(string message) : base(message)
    {
    }
}