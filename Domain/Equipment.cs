namespace ConsoleApp2.Domain;

// Klasa abstrakcyjna - część wspólna
public abstract class Equipment
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; }
    public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;

    protected Equipment(string name)
    {
        Name = name;
    }
}

// Konkretne typy sprzętu (każdy ma po 2 specyficzne pola)
public class Laptop : Equipment
{
    public int RamSizeGb { get; set; }
    public string Processor { get; set; }

    public Laptop(string name, int ramSizeGb, string processor) : base(name)
    {
        RamSizeGb = ramSizeGb;
        Processor = processor;
    }
}

public class Projector : Equipment
{
    public string Resolution { get; set; }
    public int Lumens { get; set; }

    public Projector(string name, string resolution, int lumens) : base(name)
    {
        Resolution = resolution;
        Lumens = lumens;
    }
}

public class Camera : Equipment
{
    public double Megapixels { get; set; }
    public string LensType { get; set; }

    public Camera(string name, double megapixels, string lensType) : base(name)
    {
        Megapixels = megapixels;
        LensType = lensType;
    }
}