using SmartCharging.Application.DTOs;
using SmartCharging.Application.Interfaces;
using SmartCharging.Domain.Interfaces;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Exceptions;

namespace SmartCharging.Application.Services;

public class ChargeStationService : IChargeStationService
{
    private readonly IChargeStationRepository stationRepository;
    private readonly IGroupRepository groupRepository;

    public ChargeStationService(IChargeStationRepository stationRepository, IGroupRepository groupRepository)
    {
        this.stationRepository = stationRepository;
        this.groupRepository = groupRepository;
    }

    public async Task<ChargeStationDto?> GetByIdAsync(Guid id)
    {
        ChargeStation? station = await stationRepository.GetByIdAsync(id);
        return station == null ? null : MapToDto(station);
    }

    public async Task<ChargeStationDto> CreateAsync(Guid groupId, string name, List<(int id, int maxCurrent)> connectors)
    {
        Group? group = await groupRepository.GetByIdAsync(groupId);

        if (group == null)
            throw new DomainException($"Group {groupId} not found.");

        List<Connector> connectorList = connectors
            .Select(c => new Connector(c.id, c.maxCurrent))
            .ToList();

        ChargeStation station = new ChargeStation(Guid.NewGuid(), name, connectorList);

        group.AddChargeStation(station);

        await stationRepository.AddAsync(station);
        await groupRepository.UpdateAsync(group);

        return MapToDto(station);
    }

    public async Task UpdateAsync(Guid id, string name)
    {
        ChargeStation? station = await stationRepository.GetByIdAsync(id);

        if (station == null)
            throw new DomainException($"Charge station {id} not found.");

        station.UpdateName(name);
        await stationRepository.UpdateAsync(station);
    }

    public async Task DeleteAsync(Guid id)
    {
        ChargeStation? station = await stationRepository.GetByIdAsync(id);

        if (station == null)
            throw new DomainException($"Charge station {id} not found.");

        await stationRepository.DeleteAsync(id);
    }

    public async Task UpdateConnectorAsync(Guid stationId, int connectorId, int newMaxCurrent)
    {
        ChargeStation? station = await stationRepository.GetByIdAsync(stationId);

        if (station == null)
            throw new DomainException($"Charge station {stationId} not found.");

        Connector? connector = station.Connectors.FirstOrDefault(c => c.Id == connectorId);

        if (connector == null)
            throw new DomainException($"Connector {connectorId} not found.");

        connector.UpdateMaxCurrent(newMaxCurrent);
        await stationRepository.UpdateAsync(station);
    }

    private ChargeStationDto MapToDto(ChargeStation station)
    {
        return new ChargeStationDto
        {
            Id = station.Id,
            Name = station.Name,
            Connectors = station.Connectors.Select(c => new ConnectorDto
            {
                Id = c.Id,
                MaxCurrent = c.MaxCurrent
            }).ToList()
        };
    }
}