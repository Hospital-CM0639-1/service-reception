using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace reception.Controllers;

[ApiController]
[Route("user")]
public class PatientController(IPatientService patientService): Controller
{
    [HttpGet]
    [Route("exists")]
    public async Task<IActionResult> DoesPatientExists(int id)
    {
        Console.WriteLine("-- does patient  exists endpoint");
        return Ok(await patientService.DoesPatientExist(id));
    }
}