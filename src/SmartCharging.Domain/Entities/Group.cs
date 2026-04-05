using SmartCharging.Domain.Exceptions;

namespace SmartCharging.Domain.Entities;

public class Group
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int Capacity { get; private set; }
    public List<ChargeStation> ChargeStations { get; private set; }

    public Group(Guid id, string name, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name cannot be empty.");

        if (capacity <= 0)
            throw new DomainException("Capacity must be greater than zero.");

        Id = id;
        Name = name;
        Capacity = capacity;
        ChargeStations = new List<ChargeStation>();
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new DomainException("Name cannot be empty.");

        Name = newName;
    }

    public void UpdateCapacity(int newCapacity)
    {
        if (newCapacity <= 0)
            throw new DomainException("Capacity must be greater than zero.");

        int totalLoad = ChargeStations.Sum(s => s.GetTotalCurrentLoad());

        if (newCapacity < totalLoad)
            throw new DomainException($"Cannot set capacity to {newCapacity}A, current total load is {totalLoad}A.");

        Capacity = newCapacity;
    }

    public void AddChargeStation(ChargeStation station)
    {
        int newTotal = GetCurrentLoad() + station.GetTotalCurrentLoad();

        if (newTotal > Capacity)
            throw new DomainException($"Adding this station would exceed group capacity ({newTotal}A > {Capacity}A).");

        ChargeStations.Add(station);
    }

    public void RemoveChargeStation(Guid stationId)
    {
        ChargeStation station = ChargeStations.FirstOrDefault(s => s.Id == stationId);

        if (station == null)
            throw new DomainException($"Charge station {stationId} not found in this group.");

        ChargeStations.Remove(station);
    }

    public int GetCurrentLoad()
    {
        return ChargeStations.Sum(s => s.GetTotalCurrentLoad());
    }
}