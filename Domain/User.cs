namespace ConsoleApp2.Domain;

public abstract class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public abstract int MaxActiveRentals { get; } 

    protected User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public class Student : User
{
    // Student może mieć 2 wypożyczenia
    public override int MaxActiveRentals => 2; 
    
    public Student(string firstName, string lastName) : base(firstName, lastName) { }
}

public class Employee : User
{
    // Pracownik może mieć 5 wypożyczeń
    public override int MaxActiveRentals => 5;
    
    public Employee(string firstName, string lastName) : base(firstName, lastName) { }
}