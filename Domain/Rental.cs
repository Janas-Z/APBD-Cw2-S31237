namespace ConsoleApp2.Domain;

public class Rental
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid EquipmentId { get; set; }
    public Guid UserId { get; set; }
    
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; } // Nullable, bo na początku nie ma daty zwrotu
    public decimal PenaltyFee { get; set; }

    // Wyliczana właściwość pomocnicza
    public bool IsActive => ReturnDate == null; 
}