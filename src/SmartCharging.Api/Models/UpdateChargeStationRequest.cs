using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Api.Models;

public class UpdateChargeStationRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}