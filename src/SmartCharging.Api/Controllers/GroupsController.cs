using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.Models;
using SmartCharging.Application.Interfaces;
using SmartCharging.Domain.Exceptions;

namespace SmartCharging.Api.Controllers;

[ApiController]
[Route("api/groups")]
public class GroupsController : ControllerBase
{
    private readonly IGroupService groupService;

    public GroupsController(IGroupService groupService)
    {
        this.groupService = groupService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var group = await groupService.GetByIdAsync(id);
        if (group == null) return NotFound();
        return Ok(group);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequest request)
    {
        var group = await groupService.CreateAsync(request.Name, request.Capacity);
        return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGroupRequest request)
    {
        await groupService.UpdateAsync(id, request.Name, request.Capacity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await groupService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{groupId}/charge-stations/{stationId}")]
    public async Task<IActionResult> AddChargeStation(Guid groupId, Guid stationId)
    {
        await groupService.AddChargeStationAsync(groupId, stationId);
        return NoContent();
    }

    [HttpDelete("{groupId}/charge-stations/{stationId}")]
    public async Task<IActionResult> RemoveChargeStation(Guid groupId, Guid stationId)
    {
        await groupService.RemoveChargeStationAsync(groupId, stationId);
        return NoContent();
    }
}