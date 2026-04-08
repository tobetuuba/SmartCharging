using SmartCharging.Domain.Entities;

namespace SmartCharging.Domain.Interfaces;

public interface IChargeStationRepository
{
    Task<ChargeStation?> GetByIdAsync(Guid id);
    Task AddAsync(ChargeStation station);
    Task UpdateAsync(ChargeStation station);
    Task DeleteAsync(Guid id);
}