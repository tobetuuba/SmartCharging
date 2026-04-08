using SmartCharging.Application.DTOs;
using SmartCharging.Application.Interfaces;
using SmartCharging.Domain.Interfaces;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Exceptions;

namespace SmartCharging.Application.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository groupRepository;
    private readonly IChargeStationRepository stationRepository;

    public GroupService(IGroupRepository groupRepository, IChargeStationRepository stationRepository)
    {
        this.groupRepository = groupRepository;
        this.stationRepository = stationRepository;
    }

    public async Task<GroupDto?> GetByIdAsync(Guid id)
    {
        Group? group = await groupRepository.GetByIdAsync(id);
        return group == null ? null : MapToDto(group);
    }

    public async Task<GroupDto> CreateAsync(string name, int capacity)
    {
        Group group = new Group(Guid.NewGuid(), name, capacity);
        await groupRepository.AddAsync(group);
        return MapToDto(group);
    }

    public async Task UpdateAsync(Guid id, string name, int capacity)
    {
        Group? group = await groupRepository.GetByIdAsync(id);

        if (group == null)
            throw new DomainException($"Group {id} not found.");

        group.UpdateName(name);
        group.UpdateCapacity(capacity);
        await groupRepository.UpdateAsync(group);
    }

    public async Task DeleteAsync(Guid id)
    {
        Group? group = await groupRepository.GetByIdAsync(id);

        if (group == null)
            throw new DomainException($"Group {id} not found.");

        foreach (ChargeStation station in group.ChargeStations)
            await stationRepository.DeleteAsync(station.Id);

        await groupRepository.DeleteAsync(id);
    }

    public async Task AddChargeStationAsync(Guid groupId, Guid stationId)
    {
        Group? group = await groupRepository.GetByIdAsync(groupId);

        if (group == null)
            throw new DomainException($"Group {groupId} not found.");

        ChargeStation? station = await stationRepository.GetByIdAsync(stationId);

        if (station == null)
            throw new DomainException($"Charge station {stationId} not found.");

        group.AddChargeStation(station);
        await groupRepository.UpdateAsync(group);
    }

    public async Task RemoveChargeStationAsync(Guid groupId, Guid stationId)
    {
        Group? group = await groupRepository.GetByIdAsync(groupId);

        if (group == null)
            throw new DomainException($"Group {groupId} not found.");

        group.RemoveChargeStation(stationId);
        await groupRepository.UpdateAsync(group);
    }

    private GroupDto MapToDto(Group group)
    {
        return new GroupDto
        {
            Id = group.Id,
            Name = group.Name,
            Capacity = group.Capacity,
            ChargeStations = group.ChargeStations.Select(s => new ChargeStationDto
            {
                Id = s.Id,
                Name = s.Name,
                Connectors = s.Connectors.Select(c => new ConnectorDto
                {
                    Id = c.Id,
                    MaxCurrent = c.MaxCurrent
                }).ToList()
            }).ToList()
        };
    }
}