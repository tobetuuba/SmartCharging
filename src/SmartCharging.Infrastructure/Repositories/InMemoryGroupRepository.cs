using SmartCharging.Application.Interfaces;
using SmartCharging.Domain.Entities;

namespace SmartCharging.Infrastructure.Repositories;

public class InMemoryGroupRepository : IGroupRepository
{
    private readonly Dictionary<Guid, Group> store = new();

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
        store.Remove(id);
        return Task.CompletedTask;
    }
}