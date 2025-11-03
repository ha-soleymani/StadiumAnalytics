using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SensorEventsController : ControllerBase
{
    private readonly ISensorEventService _service;

    public SensorEventsController(ISensorEventService service)
    {
        _service = service;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary([FromQuery] string? gate, [FromQuery] string? type, [FromQuery] DateTime? start, [FromQuery] DateTime? end)
    {
        var result = await _service.GetSummaryAsync(gate, type, start, end);
        return Ok(result);
    }
}