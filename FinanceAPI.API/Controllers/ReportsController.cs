using System.Security.Claims;
using FinanceAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("monthly")]
    public async Task<IActionResult> GetMonthly([FromQuery] int month, [FromQuery] int year)
    {
        if (month < 1 || month > 12)
            return BadRequest(new { message = "Mês inválido. Use um valor entre 1 e 12." });

        if (year < 2000 || year > DateTime.UtcNow.Year)
            return BadRequest(new { message = "Ano inválido." });

        var result = await _reportService.GetMonthlyReportAsync(GetUserId(), month, year);
        return Ok(result);
    }
}