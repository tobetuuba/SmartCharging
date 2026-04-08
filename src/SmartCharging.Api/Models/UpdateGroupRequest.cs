using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Api.Models;

public class UpdateGroupRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
    public int Capacity { get; set; }
}