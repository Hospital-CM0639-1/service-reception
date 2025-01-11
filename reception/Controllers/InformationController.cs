using DataAccessLayer.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace reception.Controllers;

[ApiController]
[Route("info")]
public class InformationController (IDoctorService doctorService, IManagerService managerService):Controller
{ 
    [HttpGet]
    [Route("doctor_types")]
    [ProducesResponseType(typeof(List<string?>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetTypesOfDoctors()
    {
        return Ok(await doctorService.GetDoctorSpecializationsAsync());
    }
    
    [HttpGet]
    [Route("doctors/{type}")]
    [ProducesResponseType(typeof(List<Staff>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetDoctorsByType(string type)
    {
        return Ok(await doctorService.GetDoctorsAsync(type));
    }
    
    [HttpGet]
    [Route("doctors_on_duty")]
    [ProducesResponseType(typeof(List<Staff?>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetDoctorsAvailable()
    {
        return Ok(await doctorService.GetDoctorsAsync(available: true));
    }
    
    [HttpGet]
    [Route("free_bed")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetFreeBeds()
    {
        return Ok(await managerService.HasFreeBedAsync());
    }
}