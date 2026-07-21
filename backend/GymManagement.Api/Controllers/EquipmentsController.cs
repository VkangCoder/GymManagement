using GymManagement.Api.Interfaces;
using GymManagement.Api.Mappings;
using GymManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("api/equipments")]
public class EquipmentsController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentsController(IEquipmentService equipmentService) =>
        _equipmentService = equipmentService;

    [HttpGet]
    public async Task<ActionResult<List<EquipmentResponse>>> GetAll(CancellationToken ct)
    {
        var equipments = await _equipmentService.GetAllAsync(ct);
        return Ok(equipments.Select(e => e.ToResponse()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EquipmentResponse>> GetById(string id, CancellationToken ct)
    {
        var equipment = await _equipmentService.GetByIdAsync(id, ct);
        return equipment is null ? NotFound() : Ok(equipment.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<EquipmentResponse>> CreateEquipment(
        [FromBody] CreateEquipmentRequest request, CancellationToken ct)
    {
        var created = await _equipmentService.CreateEquipmentAsync(request.ToEntity(), ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EquipmentResponse>> UpdateEquipment(
        string id, [FromBody] UpdateEquipmentRequest request, CancellationToken ct)
    {
        var updated = await _equipmentService.UpdateEquipmentAsync(id, request.ToEntity(), ct);
        return updated is null
            ? NotFound($"Equipment with ID {id} was not found.")
            : Ok(updated.ToResponse());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEquipment(string id, CancellationToken ct)
    {
        var isDeleted = await _equipmentService.DeleteEquipmentAsync(id, ct);
        return isDeleted
            ? NoContent()
            : NotFound($"Equipment with ID {id} was not found.");
    }
}