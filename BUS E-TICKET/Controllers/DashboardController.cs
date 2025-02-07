using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class DashboardController(DashboardService dashboardService) : Controller
{
    private readonly DashboardService _dashboardService = dashboardService ?? throw new ArgumentNullException(nameof(dashboardService));

    [HttpGet("stats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = "Admin")]
    public IActionResult GetStats()
    {
        var stats = _dashboardService.GetDashboardStats();
        return Ok(ResponeHelper.GetApiRespone(200, "Stats Calculator successfully", stats));
    }
}
