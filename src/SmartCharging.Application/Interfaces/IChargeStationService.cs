using SmartCharging.Application.DTOs;

namespace SmartCharging.Application.Interfaces;

public interface IChargeStationService
{
    Task<ChargeStationDto?> GetByIdAsync(Guid id);
    Task<ChargeStationDto> CreateAsync(Guid groupId, string name, List<(int id, int maxCurrent)> connectors);
    Task UpdateAsync(Guid id, string name);
    Task DeleteAsync(Guid id);
    Task UpdateConnectorAsync(Guid stationId, int connectorId, int newMaxCurrent);
}