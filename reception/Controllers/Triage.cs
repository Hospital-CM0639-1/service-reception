using Infrastructure.InputModels;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace reception.Controllers;

[ApiController]
[Route("triage")]
public class Triage(IEmergencyVisitService emergencyVisitService) : Controller
{
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> AddEmergencyVisit([FromBody] EmergencyVisit emergencyVisit)
    {
        var result = await emergencyVisitService.AddEmergencyVisitAsync(emergencyVisit);
        return result ? Ok() : BadRequest();
    }
}