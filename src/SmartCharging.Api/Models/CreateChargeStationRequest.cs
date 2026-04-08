using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Api.Models;

public class CreateChargeStationRequest
{
    [Required]
    public Guid GroupId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [MinLength(1, ErrorMessage = "At least one connector is required.")]
    [MaxLength(5, ErrorMessage = "A charge station cannot have more than 5 connectors.")]
    public List<ConnectorRequest> Connectors { get; set; } = new();
}

public class ConnectorRequest
{
    [Range(1, 5, ErrorMessage = "Connector id must be between 1 and 5.")]
    public int Id { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Max current must be greater than zero.")]
    public int MaxCurrent { get; set; }
}