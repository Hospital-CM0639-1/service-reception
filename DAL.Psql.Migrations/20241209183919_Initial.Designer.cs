﻿// <auto-generated />
using System;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Psql.Migrations.Migrations
{
    [DbContext(typeof(HospitalContext))]
    [Migration("20241209183919_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "bed_status", new[] { "AVAILABLE", "UCCUPIED", "MAINTENACE", "RESERVED" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "patient_status", new[] { "WAITING", "IN_TREATMENT", "DISCHARGED", "ADMITTED" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "staff_role", new[] { "DOCTOR", "NURSE", "SECRETARY" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Models.EmergencyVisit", b =>
                {
                    b.Property<int>("VisitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("visit_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VisitId"));

                    b.Property<DateTime?>("AdmissionTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("admission_timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CurrentStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DischargeTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("discharge_timestamp");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer")
                        .HasColumnName("patient_id");

                    b.Property<string>("PriorityLevel")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("priority_level")
                        .HasDefaultValueSql("'white'::character varying");

                    b.Property<string>("TriageNotes")
                        .HasColumnType("text")
                        .HasColumnName("triage_notes");

                    b.HasKey("VisitId")
                        .HasName("emergency_visits_pkey");

                    b.HasIndex(new[] { "PatientId" }, "idx_patient_visits");

                    b.HasIndex(new[] { "PriorityLevel" }, "idx_visit_priority");

                    b.ToTable("emergency_visits", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Models.EmergencyVisitStaff", b =>
                {
                    b.Property<int>("VisitId")
                        .HasColumnType("integer")
                        .HasColumnName("visit_id");

                    b.Property<int>("StaffId")
                        .HasColumnType("integer")
                        .HasColumnName("staff_id");

                    b.Property<int>("StaffRoleEnum")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("AssignedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("assigned_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("VisitId", "StaffId", "StaffRoleEnum")
                        .HasName("emergency_visit_staff_pkey");

                    b.HasIndex("StaffId");

                    b.HasIndex(new[] { "VisitId", "StaffId" }, "idx_emergency_visit_staff");

                    b.ToTable("EmergencyVisitStaffs");
                });

            modelBuilder.Entity("DataAccessLayer.Models.HospitalBed", b =>
                {
                    b.Property<int>("BedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("bed_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BedId"));

                    b.Property<string>("BedNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("bed_number");

                    b.Property<int>("BedStatusEnum")
                        .HasColumnType("integer");

                    b.Property<int?>("CurrentVisitId")
                        .HasColumnType("integer")
                        .HasColumnName("current_visit_id");

                    b.Property<DateTime?>("LastCleanedTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_cleaned_timestamp");

                    b.Property<string>("WardSection")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("ward_section");

                    b.HasKey("BedId")
                        .HasName("hospital_beds_pkey");

                    b.HasIndex("CurrentVisitId");

                    b.HasIndex(new[] { "WardSection", "BedNumber" }, "hospital_beds_ward_section_bed_number_key")
                        .IsUnique();

                    b.ToTable("hospital_beds", (string)null);

                    b.HasData(
                        new
                        {
                            BedId = 1,
                            BedNumber = "1",
                            BedStatusEnum = 0,
                            WardSection = "PRIORITY"
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.MedicalProcedure", b =>
                {
                    b.Property<int>("ProcedureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("procedure_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProcedureId"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("PerformedByStaffId")
                        .HasColumnType("integer")
                        .HasColumnName("performed_by_staff_id");

                    b.Property<decimal?>("ProcedureCost")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("procedure_cost");

                    b.Property<string>("ProcedureName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("procedure_name");

                    b.Property<DateTime?>("ProcedureTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("procedure_timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("VisitId")
                        .HasColumnType("integer")
                        .HasColumnName("visit_id");

                    b.HasKey("ProcedureId")
                        .HasName("medical_procedures_pkey");

                    b.HasIndex("PerformedByStaffId");

                    b.HasIndex("VisitId");

                    b.ToTable("medical_procedures", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("patient_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PatientId"));

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("ContactNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("contact_number");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("EmergencyContactName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("emergency_contact_name");

                    b.Property<string>("EmergencyContactNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("emergency_contact_number");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("first_name");

                    b.Property<string>("Gender")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("gender");

                    b.Property<string>("InsurancePolicyNumber")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("insurance_policy_number");

                    b.Property<string>("InsuranceProvider")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("insurance_provider");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("last_name");

                    b.Property<DateTime?>("LastUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("PatientId")
                        .HasName("patients_pkey");

                    b.HasIndex(new[] { "LastName", "FirstName" }, "idx_patient_name");

                    b.ToTable("patients", (string)null);

                    b.HasData(
                        new
                        {
                            PatientId = 1,
                            Address = "1234 Main St",
                            DateOfBirth = new DateOnly(1, 1, 1),
                            FirstName = "Pepe",
                            InsurancePolicyNumber = "12345",
                            LastName = "Pizza"
                        },
                        new
                        {
                            PatientId = 2,
                            Address = "1000 Main St",
                            DateOfBirth = new DateOnly(1, 1, 1),
                            FirstName = "Tom",
                            InsurancePolicyNumber = "12346",
                            LastName = "Fredy"
                        },
                        new
                        {
                            PatientId = 3,
                            Address = "5000 Main St",
                            DateOfBirth = new DateOnly(1, 1, 1),
                            FirstName = "Sofia",
                            InsurancePolicyNumber = "12347",
                            LastName = "Falcony"
                        },
                        new
                        {
                            PatientId = 4,
                            Address = "3333 Haf Baf",
                            DateOfBirth = new DateOnly(1, 1, 1),
                            FirstName = "Labra",
                            InsurancePolicyNumber = "12345",
                            LastName = "Dor"
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.PatientInvoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("invoice_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("InvoiceId"));

                    b.Property<int>("CreatedByStaffId")
                        .HasColumnType("integer")
                        .HasColumnName("created_by_staff_id");

                    b.Property<DateTime?>("InvoiceTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("invoice_timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<decimal?>("PaymentReceivedAmount")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("payment_received_amount")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime?>("PaymentReceivedTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("payment_received_timestamp");

                    b.Property<string>("PaymentStatus")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("payment_status")
                        .HasDefaultValueSql("'pending'::character varying");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("total_amount");

                    b.Property<int>("VisitId")
                        .HasColumnType("integer")
                        .HasColumnName("visit_id");

                    b.HasKey("InvoiceId")
                        .HasName("patient_invoices_pkey");

                    b.HasIndex("CreatedByStaffId");

                    b.HasIndex("VisitId");

                    b.ToTable("patient_invoices", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Models.PatientVital", b =>
                {
                    b.Property<int>("VitalRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("vital_record_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VitalRecordId"));

                    b.Property<string>("AdditionalObservations")
                        .HasColumnType("text")
                        .HasColumnName("additional_observations");

                    b.Property<int?>("BloodPressureDiastolic")
                        .HasColumnType("integer")
                        .HasColumnName("blood_pressure_diastolic");

                    b.Property<int?>("BloodPressureSystolic")
                        .HasColumnType("integer")
                        .HasColumnName("blood_pressure_systolic");

                    b.Property<decimal?>("BodyTemperature")
                        .HasPrecision(4, 1)
                        .HasColumnType("numeric(4,1)")
                        .HasColumnName("body_temperature");

                    b.Property<int?>("HeartRate")
                        .HasColumnType("integer")
                        .HasColumnName("heart_rate");

                    b.Property<decimal?>("OxygenSaturation")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)")
                        .HasColumnName("oxygen_saturation");

                    b.Property<DateTime?>("RecordedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("recorded_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("RecordedByStaffId")
                        .HasColumnType("integer")
                        .HasColumnName("recorded_by_staff_id");

                    b.Property<int?>("RespiratoryRate")
                        .HasColumnType("integer")
                        .HasColumnName("respiratory_rate");

                    b.Property<int>("VisitId")
                        .HasColumnType("integer")
                        .HasColumnName("visit_id");

                    b.HasKey("VitalRecordId")
                        .HasName("patient_vitals_pkey");

                    b.HasIndex("RecordedByStaffId");

                    b.HasIndex("VisitId");

                    b.ToTable("patient_vitals", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Models.PriorityLevel", b =>
                {
                    b.Property<string>("PriorityCode")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("priority_code");

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("color_code");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("integer")
                        .HasColumnName("display_order");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<string>("PriorityName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("priority_name");

                    b.HasKey("PriorityCode")
                        .HasName("priority_levels_pkey");

                    b.ToTable("priority_levels", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Staff", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("staff_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StaffId"));

                    b.Property<string>("Department")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("department");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("first_name");

                    b.Property<DateOnly>("HireDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("hire_date")
                        .HasDefaultValueSql("CURRENT_DATE");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("last_name");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("phone_number");

                    b.Property<int>("RoleEnum")
                        .HasColumnType("integer");

                    b.Property<string>("Specialization")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("specialization");

                    b.HasKey("StaffId")
                        .HasName("staff_pkey");

                    b.HasIndex(new[] { "Email" }, "staff_email_key")
                        .IsUnique();

                    b.ToTable("staff", (string)null);

                    b.HasData(
                        new
                        {
                            StaffId = 1,
                            Email = "contact@email.com",
                            FirstName = "Doctor",
                            HireDate = new DateOnly(1, 1, 1),
                            LastName = "1",
                            RoleEnum = 0
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.EmergencyVisit", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Patient", "Patient")
                        .WithMany("EmergencyVisits")
                        .HasForeignKey("PatientId")
                        .IsRequired()
                        .HasConstraintName("emergency_visits_patient_id_fkey");

                    b.HasOne("DataAccessLayer.Models.PriorityLevel", "PriorityLevelNavigation")
                        .WithMany("EmergencyVisits")
                        .HasForeignKey("PriorityLevel")
                        .IsRequired()
                        .HasConstraintName("emergency_visits_priority_level_fkey");

                    b.Navigation("Patient");

                    b.Navigation("PriorityLevelNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Models.EmergencyVisitStaff", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .IsRequired()
                        .HasConstraintName("emergency_visit_staff_staff_id_fkey");

                    b.HasOne("DataAccessLayer.Models.EmergencyVisit", "Visit")
                        .WithMany()
                        .HasForeignKey("VisitId")
                        .IsRequired()
                        .HasConstraintName("emergency_visit_staff_visit_id_fkey");

                    b.Navigation("Staff");

                    b.Navigation("Visit");
                });

            modelBuilder.Entity("DataAccessLayer.Models.HospitalBed", b =>
                {
                    b.HasOne("DataAccessLayer.Models.EmergencyVisit", "CurrentVisit")
                        .WithMany("HospitalBeds")
                        .HasForeignKey("CurrentVisitId")
                        .HasConstraintName("hospital_beds_current_visit_id_fkey");

                    b.Navigation("CurrentVisit");
                });

            modelBuilder.Entity("DataAccessLayer.Models.MedicalProcedure", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Staff", "PerformedByStaff")
                        .WithMany("MedicalProcedures")
                        .HasForeignKey("PerformedByStaffId")
                        .IsRequired()
                        .HasConstraintName("medical_procedures_performed_by_staff_id_fkey");

                    b.HasOne("DataAccessLayer.Models.EmergencyVisit", "Visit")
                        .WithMany("MedicalProcedures")
                        .HasForeignKey("VisitId")
                        .IsRequired()
                        .HasConstraintName("medical_procedures_visit_id_fkey");

                    b.Navigation("PerformedByStaff");

                    b.Navigation("Visit");
                });

            modelBuilder.Entity("DataAccessLayer.Models.PatientInvoice", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Staff", "CreatedByStaff")
                        .WithMany("PatientInvoices")
                        .HasForeignKey("CreatedByStaffId")
                        .IsRequired()
                        .HasConstraintName("patient_invoices_created_by_staff_id_fkey");

                    b.HasOne("DataAccessLayer.Models.EmergencyVisit", "Visit")
                        .WithMany("PatientInvoices")
                        .HasForeignKey("VisitId")
                        .IsRequired()
                        .HasConstraintName("patient_invoices_visit_id_fkey");

                    b.Navigation("CreatedByStaff");

                    b.Navigation("Visit");
                });

            modelBuilder.Entity("DataAccessLayer.Models.PatientVital", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Staff", "RecordedByStaff")
                        .WithMany("PatientVitals")
                        .HasForeignKey("RecordedByStaffId")
                        .IsRequired()
                        .HasConstraintName("patient_vitals_recorded_by_staff_id_fkey");

                    b.HasOne("DataAccessLayer.Models.EmergencyVisit", "Visit")
                        .WithMany("PatientVitals")
                        .HasForeignKey("VisitId")
                        .IsRequired()
                        .HasConstraintName("patient_vitals_visit_id_fkey");

                    b.Navigation("RecordedByStaff");

                    b.Navigation("Visit");
                });

            modelBuilder.Entity("DataAccessLayer.Models.EmergencyVisit", b =>
                {
                    b.Navigation("HospitalBeds");

                    b.Navigation("MedicalProcedures");

                    b.Navigation("PatientInvoices");

                    b.Navigation("PatientVitals");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Patient", b =>
                {
                    b.Navigation("EmergencyVisits");
                });

            modelBuilder.Entity("DataAccessLayer.Models.PriorityLevel", b =>
                {
                    b.Navigation("EmergencyVisits");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Staff", b =>
                {
                    b.Navigation("MedicalProcedures");

                    b.Navigation("PatientInvoices");

                    b.Navigation("PatientVitals");
                });
#pragma warning restore 612, 618
        }
    }
}
