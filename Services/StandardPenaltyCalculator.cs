namespace ConsoleApp2.Services;

public class StandardPenaltyCalculator : IPenaltyCalculator
{
    private const decimal DailyPenaltyRate = 5.0m; // 5 zł za każdy dzień opóźnienia

    public decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate <= dueDate) return 0m; // Zwrot w terminie = brak kary
        
        int daysLate = (returnDate - dueDate).Days;
        return daysLate * DailyPenaltyRate;
    }
}