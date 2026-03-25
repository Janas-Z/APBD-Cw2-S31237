using ConsoleApp2.Domain;
using ConsoleApp2.Exceptions;
using ConsoleApp2.Repositories;
using ConsoleApp2.Services;

// 1. Inicjalizacja systemu (Dependency Injection bez użycia zewn. bibliotek)
var db = new InMemoryDatabase();
var penaltyCalculator = new StandardPenaltyCalculator();
var rentalService = new RentalService(db, penaltyCalculator);

Console.WriteLine("--- UCZELNIANA WYPOŻYCZALNIA SPRZĘTU ---");

// 11. Dodanie kilku egzemplarzy sprzętu różnych typów
var laptop = new Laptop("Dell XPS 15", 16, "Intel Core i7");
var projector = new Projector("Epson 1080p", "1920x1080", 3000);
var camera = new Camera("Sony A7 III", 24.2, "28-70mm");
db.Equipments.Add(laptop);
db.Equipments.Add(projector);
db.Equipments.Add(camera);

// 12. Dodanie kilku użytkowników różnych typów
var student = new Student("Jan", "Kowalski");
var employee = new Employee("Anna", "Nowak");
db.Users.Add(student);
db.Users.Add(employee);

// 13. Poprawne wypożyczenie sprzętu
Console.WriteLine("\n[13] Poprawne wypożyczenie sprzętu:");
rentalService.RentEquipment(student, laptop, 7); // Wypożyczenie na 7 dni
Console.WriteLine($"Student {student.FirstName} wypożyczył {laptop.Name}. Status sprzętu: {laptop.Status}");

// 14. Próba wykonania niepoprawnej operacji (sprzęt niedostępny)
Console.WriteLine("\n[14] Próba niepoprawnej operacji (wypożyczenie zajętego sprzętu):");
try
{
    rentalService.RentEquipment(employee, laptop, 3);
}
catch (EquipmentUnavailableException ex)
{
    Console.WriteLine($"[OCZEKIWANY BŁĄD] {ex.Message}");
}

// 15. Zwrot sprzętu w terminie
Console.WriteLine("\n[15] Zwrot sprzętu w terminie (Brak kary):");
rentalService.RentEquipment(employee, projector, 5); // Pracownik bierze rzutnik na 5 dni
var penalty1 = rentalService.ReturnEquipment(employee, projector, DateTime.Now.AddDays(2)); // Oddaje po 2 dniach
Console.WriteLine($"Pracownik zwrócił rzutnik w terminie. Naliczona kara: {penalty1} PLN");

// 16. Zwrot opóźniony skutkujący naliczeniem kary
Console.WriteLine("\n[16] Zwrot opóźniony skutkujący naliczeniem kary:");
// Zwracamy laptopa po 10 dniach (zamiast po 7) - 3 dni opóźnienia * 5 PLN/dzień = 15 PLN
var penalty2 = rentalService.ReturnEquipment(student, laptop, DateTime.Now.AddDays(10));
Console.WriteLine($"Student zwrócił {laptop.Name} po terminie! Naliczona kara: {penalty2} PLN. Aktualny status: {laptop.Status}");

// 17. Wyświetlenie raportu końcowego o stanie systemu
Console.WriteLine("\n[17] Raport końcowy o stanie systemu:");
Console.WriteLine($"- Liczba urządzeń w systemie: {db.Equipments.Count}");
Console.WriteLine($"- Urządzenia dostępne: {db.Equipments.Count(e => e.Status == EquipmentStatus.Available)}");
Console.WriteLine($"- Urządzenia w użyciu: {db.Equipments.Count(e => e.Status == EquipmentStatus.Rented)}");
Console.WriteLine($"- Zarejestrowani użytkownicy: {db.Users.Count}");
Console.WriteLine($"- Suma naliczonych kar w systemie: {db.Rentals.Sum(r => r.PenaltyFee)} PLN");