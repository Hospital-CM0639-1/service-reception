﻿using DataAccessLayer.Models;
using Infrastructure.InputModels;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reception.OutputModels;
using reception.QueryParameters;

namespace reception.Controllers;

[ApiController]
[Route("patient")]
[Authorize]
public class PatientController(IPatientService patientService): Controller
{
    [HttpGet]
    [Route("exists")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    public async Task<IActionResult> DoesPatientExists(int id)
    {
        return Ok(await patientService.DoesPatientExist(id));
    }
    
    [HttpGet]
    [Route("get")]
    [ProducesResponseType(typeof(Patient), StatusCodes.Status201Created)]
    public async Task<IActionResult?> GetPatientById(int id)
    {
        return Ok(await patientService.GetPatientByIdAsync(id));
    }
    
    [HttpGet]
    [Route("getall")]
    [ProducesResponseType(typeof(OutputGetAll), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetPatients([FromQuery]PatientGetFilterQueryParameters filterQueryParameters)
    {
        var patients = await patientService.GetPatientsAsync(filterQueryParameters);
        var totalPatientsCount = await patientService.GetTotalPatientsCountAsync();
        var totalPages = (int)Math.Ceiling((double)totalPatientsCount / filterQueryParameters.Number);
        
        var result = new OutputGetAll
        {
            Content = patients,
            TotalElements = totalPatientsCount,
            TotalPages = totalPages,
            Number = filterQueryParameters.Page,
            Size = filterQueryParameters.Number
        };
        return Ok(result);
    }
    
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddPatient([FromBody] RegistrationPatient patient)
    {
        var output = await patientService.AddPatientAsync(patient);
        if (!output) return BadRequest(new {Message = "Patient could not be added.", patient});
        return Ok();
    }
    
    [HttpPut]
    [Route("update")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdatePatient([FromBody] RegistrationPatient patient)
    {
        var output = await patientService.UpdatePatientAsync(patient);
        if (!output) return BadRequest(new {Message = "Patient could not be updated.", patient});
        return Ok();
    }
}