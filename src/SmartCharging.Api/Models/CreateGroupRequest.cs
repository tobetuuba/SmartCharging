namespace SmartCharging.Api.Models;

public class CreateGroupRequest
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
}