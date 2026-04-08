using SmartCharging.Domain.Entities;

namespace SmartCharging.Domain.Interfaces;

public interface IGroupRepository
{
    Task<Group?> GetByIdAsync(Guid id);
    Task AddAsync(Group group);
    Task UpdateAsync(Group group);
    Task DeleteAsync(Guid id);
}