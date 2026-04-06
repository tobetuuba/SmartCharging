using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.Models;
using SmartCharging.Application.Interfaces;

namespace SmartCharging.Api.Controllers;

[ApiController]
[Route("api/charge-stations")]
public class ChargeStationsController : ControllerBase
{
    private readonly IChargeStationService stationService;

    public ChargeStationsController(IChargeStationService stationService)
    {
        this.stationService = stationService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var station = await stationService.GetByIdAsync(id);
        if (station == null) return NotFound();
        return Ok(station);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateChargeStationRequest request)
    {
        var connectors = request.Connectors
            .Select(c => (c.Id, c.MaxCurrent))
            .ToList();

        var station = await stationService.CreateAsync(request.GroupId, request.Name, connectors);
        return CreatedAtAction(nameof(GetById), new { id = station.Id }, station);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateChargeStationRequest request)
    {
        await stationService.UpdateAsync(id, request.Name);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await stationService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{stationId}/connectors/{connectorId}")]
    public async Task<IActionResult> UpdateConnector(Guid stationId, int connectorId, [FromBody] UpdateConnectorRequest request)
    {
        await stationService.UpdateConnectorAsync(stationId, connectorId, request.MaxCurrent);
        return NoContent();
    }
}