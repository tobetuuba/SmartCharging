using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Api.Models;

public class CreateGroupRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
    public int Capacity { get; set; }
}