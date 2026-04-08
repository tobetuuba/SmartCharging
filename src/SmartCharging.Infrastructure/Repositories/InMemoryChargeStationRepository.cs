using System.Collections.Concurrent;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;

namespace SmartCharging.Infrastructure.Repositories;

public class InMemoryChargeStationRepository : IChargeStationRepository
{
    private readonly ConcurrentDictionary<Guid, ChargeStation> store = new();

    public Task<ChargeStation?> GetByIdAsync(Guid id)
    {
        store.TryGetValue(id, out ChargeStation? station);
        return Task.FromResult(station);
    }

    public Task AddAsync(ChargeStation station)
    {
        store[station.Id] = station;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(ChargeStation station)
    {
        store[station.Id] = station;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        store.TryRemove(id, out _);
        return Task.CompletedTask;
    }
}