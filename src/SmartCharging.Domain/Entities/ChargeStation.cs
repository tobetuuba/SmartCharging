using SmartCharging.Domain.Exceptions;

namespace SmartCharging.Domain.Entities;

public class ChargeStation
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public List<Connector> Connectors { get; private set; }

    public ChargeStation(Guid id, string name, List<Connector> initialConnectors)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name cannot be empty.");

        if (initialConnectors == null || initialConnectors.Count == 0)
            throw new DomainException("A charge station must have at least one connector.");

        if (initialConnectors.Count > 5)
            throw new DomainException("A charge station cannot have more than 5 connectors.");

        Id = id;
        Name = name;
        Connectors = initialConnectors;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new DomainException("Name cannot be empty.");

        Name = newName;
    }

    public int GetTotalCurrentLoad()
    {
        return Connectors.Sum(c => c.MaxCurrent);
    }
}