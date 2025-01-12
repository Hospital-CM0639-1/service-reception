using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Infrastructure.InputModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmergencyVisitService(HospitalContext hospitalContext)
    : BaseService(hospitalContext), IEmergencyVisitService
{
    public async Task<bool> AddEmergencyVisitAsync(EmergencyVisitInput emergencyVisit)
    {
        var patient = await hospitalContext.Patients.FindAsync(emergencyVisit.PatientId);
        if (patient == null)
        {
            return false;
        }
            
        var sql = "INSERT INTO emergency_visits (Patient_Id, Current_Status, Priority_Level, Triage_Notes) " +
                  "VALUES (@p0, @p1::patient_status, @p2, @p3)";

        await hospitalContext.Database.ExecuteSqlRawAsync(sql, emergencyVisit.PatientId, emergencyVisit.Status,
            emergencyVisit.PriorityLevel.ToLower(), emergencyVisit.TriageNotes);
        
        var xx = hospitalContext.EmergencyVisits.Where(p => p.PatientId == emergencyVisit.PatientId)
            .OrderByDescending(p => p.AdmissionTimestamp)
            .FirstOrDefault();
        
        if (xx != null && emergencyVisit.DoctorId != null)
        {
            var sql2 = "INSERT INTO emergency_visit_staff (staff_role, visit_id, staff_id) " +
                "VALUES ('DOCTOR'::staff_role, @p0, @p1)";
            
            await hospitalContext.Database.ExecuteSqlRawAsync(sql2, xx.VisitId, emergencyVisit.DoctorId.Value);
        }
        return true;
    }
}