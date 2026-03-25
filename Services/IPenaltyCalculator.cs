namespace ConsoleApp2.Services;

public interface IPenaltyCalculator
{
    decimal CalculatePenalty(DateTime dueDate, DateTime returnDate);
}