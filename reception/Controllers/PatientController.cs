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
    
    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetPatientById(int id)
    {
        return Ok(await patientService.GetPatientByIdAsync(id));
    }
    
    [HttpGet]
    [Route("getall")]
    public async Task<IActionResult> GetPatients(int number, int page)
    {
        return Ok(await patientService.GetPatientsAsync(number, page));
    }
}