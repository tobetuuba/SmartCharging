namespace SmartCharging.Api.Models;

public class UpdateGroupRequest
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
}