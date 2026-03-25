using ConsoleApp2.Domain;
using ConsoleApp2.Exceptions;
using ConsoleApp2.Repositories;

namespace ConsoleApp2.Services;

public class RentalService
{
    private readonly InMemoryDatabase _db;
    private readonly IPenaltyCalculator _penaltyCalculator;

    // Wstrzykujemy bazę i kalkulator przez konstruktor (Dependency Injection)
    public RentalService(InMemoryDatabase db, IPenaltyCalculator penaltyCalculator)
    {
        _db = db;
        _penaltyCalculator = penaltyCalculator;
    }

    public void RentEquipment(User user, Equipment equipment, int days)
    {
        if (equipment.Status != EquipmentStatus.Available)
            throw new EquipmentUnavailableException($"Sprzęt {equipment.Name} jest obecnie niedostępny.");

        var activeRentals = _db.Rentals.Count(r => r.UserId == user.Id && r.IsActive);
        if (activeRentals >= user.MaxActiveRentals)
            throw new RentalLimitExceededException($"Użytkownik {user.FirstName} osiągnął limit {user.MaxActiveRentals} wypożyczeń.");

        var rental = new Rental
        {
            EquipmentId = equipment.Id,
            UserId = user.Id,
            RentDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(days)
        };

        equipment.Status = EquipmentStatus.Rented;
        _db.Rentals.Add(rental);
    }

    public decimal ReturnEquipment(User user, Equipment equipment, DateTime returnDate)
    {
        var rental = _db.Rentals.FirstOrDefault(r => r.UserId == user.Id && r.EquipmentId == equipment.Id && r.IsActive);
        
        if (rental == null)
            throw new InvalidOperationException("Nie znaleziono aktywnego wypożyczenia dla tego użytkownika i sprzętu.");

        rental.ReturnDate = returnDate;
        equipment.Status = EquipmentStatus.Available;

        // Liczymy karę i zapisujemy ją w wypożyczeniu
        rental.PenaltyFee = _penaltyCalculator.CalculatePenalty(rental.DueDate, returnDate);
        return rental.PenaltyFee;
    }
}