using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Api.Models;

public class UpdateConnectorRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Max current must be greater than zero.")]
    public int MaxCurrent { get; set; }
}