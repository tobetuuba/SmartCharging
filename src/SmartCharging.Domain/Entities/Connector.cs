using SmartCharging.Domain.Exceptions;

namespace SmartCharging.Domain.Entities;

public class Connector
{
    public int Id { get; private set; }
    public int MaxCurrent { get; private set; }

    public Connector(int id, int maxCurrent)
    {
        if (id < 1 || id > 5)
            throw new DomainException("Connector id must be between 1 and 5.");

        if (maxCurrent <= 0)
            throw new DomainException("Max current must be greater than zero.");

        Id = id;
        MaxCurrent = maxCurrent;
    }

    public void UpdateMaxCurrent(int newMaxCurrent)
    {
        if (newMaxCurrent <= 0)
            throw new DomainException("Max current must be greater than zero.");

        MaxCurrent = newMaxCurrent;
    }
}