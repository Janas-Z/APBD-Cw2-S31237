using ConsoleApp2.Domain;

namespace ConsoleApp2.Repositories;

public class InMemoryDatabase
{
    public List<User> Users { get; } = new();
    public List<Equipment> Equipments { get; } = new();
    public List<Rental> Rentals { get; } = new();
}