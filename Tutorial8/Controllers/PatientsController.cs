using Microsoft.AspNetCore.Mvc;
using Tutorial8.DTOs;
using Tutorial8.Exceptions;
using Tutorial8.Services;

namespace Tutorial8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IDbService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPatients([FromQuery] string? search, CancellationToken ct)
    {
        var result = await service.GetPatientsAsync(search, ct);
        return Ok(result);
    }

    [HttpPost("{pesel}/bedassignments")]
    public async Task<IActionResult> AssignBed(
        [FromRoute] string pesel, 
        [FromBody] AssignBedRequestDto request, 
        CancellationToken ct)
    {
        try
        {
            await service.AssignBedAsync(pesel, request, ct);
            return Created();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}