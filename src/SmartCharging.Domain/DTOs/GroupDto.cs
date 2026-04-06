namespace SmartCharging.Application.DTOs;

public class GroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public List<ChargeStationDto> ChargeStations { get; set; } = new();
}