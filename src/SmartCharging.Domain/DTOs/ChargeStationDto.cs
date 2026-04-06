namespace SmartCharging.Application.DTOs;

public class ChargeStationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ConnectorDto> Connectors { get; set; } = new();
}