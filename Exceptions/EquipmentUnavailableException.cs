namespace ConsoleApp2.Exceptions;

public class EquipmentUnavailableException : Exception
{
    public EquipmentUnavailableException(string message) : base(message)
    {
    }
}