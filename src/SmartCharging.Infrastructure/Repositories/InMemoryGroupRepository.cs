using System.Collections.Concurrent;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;

namespace SmartCharging.Infrastructure.Repositories;

public class InMemoryGroupRepository : IGroupRepository
{
    private readonly ConcurrentDictionary<Guid, Group> store = new();

    public Task<Group?> GetByIdAsync(Guid id)
    {
        store.TryGetValue(id, out Group? group);
        return Task.FromResult(group);
    }

    public Task AddAsync(Group group)
    {
        store[group.Id] = group;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Group group)
    {
        store[group.Id] = group;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        store.TryRemove(id, out _);
        return Task.CompletedTask;
    }
}