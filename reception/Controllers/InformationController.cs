using DataAccessLayer.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reception.OutputModels;

namespace reception.Controllers;

[ApiController]
[Route("info")]
[Authorize]
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
    [Route("get_doctors")]
    [ProducesResponseType(typeof(List<OutputDoctorInfo?>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetDoctorsAvailable()
    {
        var doctors = await doctorService.GetDoctorsAsync();
        var doctorInfoList = doctors.Select(d => new OutputDoctorInfo{
            Id = d.StaffId,
            Name = $"{d.FirstName} {d.LastName}",
            Department = d.Specialization ?? "General"
        }).ToList();

        return Ok(doctorInfoList);
    }
    
    [HttpGet]
    [Route("free_bed")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetFreeBeds()
    {
        return Ok(await managerService.HasFreeBedAsync());
    }
}