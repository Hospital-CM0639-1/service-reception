using System;
using System.Collections.Generic;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public partial class HospitalContext : DbContext
{
    public HospitalContext()
    {
    }

    public HospitalContext(DbContextOptions<HospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmergencyVisit> EmergencyVisits { get; set; }

    public virtual DbSet<EmergencyVisitStaff> EmergencyVisitStaffs { get; set; }

    public virtual DbSet<HospitalBed> HospitalBeds { get; set; }

    public virtual DbSet<MedicalProcedure> MedicalProcedures { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientInvoice> PatientInvoices { get; set; }

    public virtual DbSet<PatientVital> PatientVitals { get; set; }

    public virtual DbSet<PriorityLevel> PriorityLevels { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=hospital");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("bed_status", new[] { "AVAILABLE", "UCCUPIED", "MAINTENACE", "RESERVED" })
            .HasPostgresEnum("patient_status", new[] { "WAITING", "IN_TREATMENT", "DISCHARGED", "ADMITTED" })
            .HasPostgresEnum("staff_role", new[] { "DOCTOR", "NURSE", "SECRETARY" });

        modelBuilder.Entity<EmergencyVisit>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("emergency_visits_pkey");

            entity.ToTable("emergency_visits");

            entity.HasIndex(e => e.PatientId, "idx_patient_visits");

            entity.HasIndex(e => e.PriorityLevel, "idx_visit_priority");

            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.AdmissionTimestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("admission_timestamp");
            entity.Property(e => e.DischargeTimestamp).HasColumnName("discharge_timestamp");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PriorityLevel)
                .HasMaxLength(20)
                .HasDefaultValueSql("'white'::character varying")
                .HasColumnName("priority_level");
            entity.Property(e => e.TriageNotes).HasColumnName("triage_notes");

            entity.HasOne(d => d.Patient).WithMany(p => p.EmergencyVisits)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("emergency_visits_patient_id_fkey");

            entity.HasOne(d => d.PriorityLevelNavigation).WithMany(p => p.EmergencyVisits)
                .HasForeignKey(d => d.PriorityLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("emergency_visits_priority_level_fkey");
        });

        modelBuilder.Entity<EmergencyVisitStaff>(entity =>
        {
            entity.HasKey(e=> new { e.VisitId, e.StaffId, StaffRole = e.StaffRoleEnum }).HasName("emergency_visit_staff_pkey"); //ag

            entity.HasIndex(e => new { e.VisitId, e.StaffId }, "idx_emergency_visit_staff");

            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("assigned_at");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.Staff).WithMany()
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("emergency_visit_staff_staff_id_fkey");

            entity.HasOne(d => d.Visit).WithMany()
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("emergency_visit_staff_visit_id_fkey");
        });

        modelBuilder.Entity<HospitalBed>(entity =>
        {
            entity.HasKey(e => e.BedId).HasName("hospital_beds_pkey");

            entity.ToTable("hospital_beds");

            entity.HasIndex(e => new { e.WardSection, e.BedNumber }, "hospital_beds_ward_section_bed_number_key").IsUnique();

            entity.Property(e => e.BedId).HasColumnName("bed_id");
            entity.Property(e => e.BedNumber)
                .HasMaxLength(20)
                .HasColumnName("bed_number");
            entity.Property(e => e.CurrentVisitId).HasColumnName("current_visit_id");
            entity.Property(e => e.LastCleanedTimestamp).HasColumnName("last_cleaned_timestamp");
            entity.Property(e => e.WardSection)
                .HasMaxLength(50)
                .HasColumnName("ward_section");

            entity.HasOne(d => d.CurrentVisit).WithMany(p => p.HospitalBeds)
                .HasForeignKey(d => d.CurrentVisitId)
                .HasConstraintName("hospital_beds_current_visit_id_fkey");
        });

        modelBuilder.Entity<MedicalProcedure>(entity =>
        {
            entity.HasKey(e => e.ProcedureId).HasName("medical_procedures_pkey");

            entity.ToTable("medical_procedures");

            entity.Property(e => e.ProcedureId).HasColumnName("procedure_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.PerformedByStaffId).HasColumnName("performed_by_staff_id");
            entity.Property(e => e.ProcedureCost)
                .HasPrecision(10, 2)
                .HasColumnName("procedure_cost");
            entity.Property(e => e.ProcedureName)
                .HasMaxLength(255)
                .HasColumnName("procedure_name");
            entity.Property(e => e.ProcedureTimestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("procedure_timestamp");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.PerformedByStaff).WithMany(p => p.MedicalProcedures)
                .HasForeignKey(d => d.PerformedByStaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medical_procedures_performed_by_staff_id_fkey");

            entity.HasOne(d => d.Visit).WithMany(p => p.MedicalProcedures)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medical_procedures_visit_id_fkey");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("patients_pkey");

            entity.ToTable("patients");

            entity.HasIndex(e => new { e.LastName, e.FirstName }, "idx_patient_name");

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .HasColumnName("contact_number");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EmergencyContactName)
                .HasMaxLength(200)
                .HasColumnName("emergency_contact_name");
            entity.Property(e => e.EmergencyContactNumber)
                .HasMaxLength(20)
                .HasColumnName("emergency_contact_number");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("gender");
            entity.Property(e => e.InsurancePolicyNumber)
                .HasMaxLength(100)
                .HasColumnName("insurance_policy_number");
            entity.Property(e => e.InsuranceProvider)
                .HasMaxLength(100)
                .HasColumnName("insurance_provider");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("last_updated");
        });

        modelBuilder.Entity<PatientInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("patient_invoices_pkey");

            entity.ToTable("patient_invoices");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.CreatedByStaffId).HasColumnName("created_by_staff_id");
            entity.Property(e => e.InvoiceTimestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("invoice_timestamp");
            entity.Property(e => e.PaymentReceivedAmount)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("payment_received_amount");
            entity.Property(e => e.PaymentReceivedTimestamp).HasColumnName("payment_received_timestamp");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("payment_status");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10, 2)
                .HasColumnName("total_amount");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.CreatedByStaff).WithMany(p => p.PatientInvoices)
                .HasForeignKey(d => d.CreatedByStaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patient_invoices_created_by_staff_id_fkey");

            entity.HasOne(d => d.Visit).WithMany(p => p.PatientInvoices)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patient_invoices_visit_id_fkey");
        });

        modelBuilder.Entity<PatientVital>(entity =>
        {
            entity.HasKey(e => e.VitalRecordId).HasName("patient_vitals_pkey");

            entity.ToTable("patient_vitals");

            entity.Property(e => e.VitalRecordId).HasColumnName("vital_record_id");
            entity.Property(e => e.AdditionalObservations).HasColumnName("additional_observations");
            entity.Property(e => e.BloodPressureDiastolic).HasColumnName("blood_pressure_diastolic");
            entity.Property(e => e.BloodPressureSystolic).HasColumnName("blood_pressure_systolic");
            entity.Property(e => e.BodyTemperature)
                .HasPrecision(4, 1)
                .HasColumnName("body_temperature");
            entity.Property(e => e.HeartRate).HasColumnName("heart_rate");
            entity.Property(e => e.OxygenSaturation)
                .HasPrecision(5, 2)
                .HasColumnName("oxygen_saturation");
            entity.Property(e => e.RecordedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("recorded_at");
            entity.Property(e => e.RecordedByStaffId).HasColumnName("recorded_by_staff_id");
            entity.Property(e => e.RespiratoryRate).HasColumnName("respiratory_rate");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.RecordedByStaff).WithMany(p => p.PatientVitals)
                .HasForeignKey(d => d.RecordedByStaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patient_vitals_recorded_by_staff_id_fkey");

            entity.HasOne(d => d.Visit).WithMany(p => p.PatientVitals)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patient_vitals_visit_id_fkey");
        });

        modelBuilder.Entity<PriorityLevel>(entity =>
        {
            entity.HasKey(e => e.PriorityCode).HasName("priority_levels_pkey");

            entity.ToTable("priority_levels");

            entity.Property(e => e.PriorityCode)
                .HasMaxLength(20)
                .HasColumnName("priority_code");
            entity.Property(e => e.ColorCode)
                .HasMaxLength(7)
                .HasColumnName("color_code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.PriorityName)
                .HasMaxLength(50)
                .HasColumnName("priority_name");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("staff_pkey");

            entity.ToTable("staff");

            entity.HasIndex(e => e.Email, "staff_email_key").IsUnique();

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .HasColumnName("department");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("hire_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .HasColumnName("specialization");
        });

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
