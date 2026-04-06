using SmartCharging.Application.DTOs;

namespace SmartCharging.Application.Interfaces;

public interface IGroupService
{
    Task<GroupDto?> GetByIdAsync(Guid id);
    Task<GroupDto> CreateAsync(string name, int capacity);
    Task UpdateAsync(Guid id, string name, int capacity);
    Task DeleteAsync(Guid id);
    Task AddChargeStationAsync(Guid groupId, Guid stationId);
    Task RemoveChargeStationAsync(Guid groupId, Guid stationId);
}