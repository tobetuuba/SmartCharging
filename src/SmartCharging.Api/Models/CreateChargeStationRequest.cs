namespace SmartCharging.Api.Models;

public class CreateChargeStationRequest
{
    public Guid GroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ConnectorRequest> Connectors { get; set; } = new();
}

public class ConnectorRequest
{
    public int Id { get; set; }
    public int MaxCurrent { get; set; }
}