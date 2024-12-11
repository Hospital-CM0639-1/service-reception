using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Psql.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:bed_status", "AVAILABLE,UCCUPIED,MAINTENACE,RESERVED")
                .Annotation("Npgsql:Enum:patient_status", "WAITING,IN_TREATMENT,DISCHARGED,ADMITTED")
                .Annotation("Npgsql:Enum:staff_role", "DOCTOR,NURSE,SECRETARY");

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    patient_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    contact_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    emergency_contact_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    emergency_contact_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    insurance_provider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    insurance_policy_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    last_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("patients_pkey", x => x.patient_id);
                });

            migrationBuilder.CreateTable(
                name: "priority_levels",
                columns: table => new
                {
                    priority_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    priority_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    color_code = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("priority_levels_pkey", x => x.priority_code);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    staff_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    specialization = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    hire_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    is_active = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    RoleEnum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("staff_pkey", x => x.staff_id);
                });

            migrationBuilder.CreateTable(
                name: "emergency_visits",
                columns: table => new
                {
                    visit_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    admission_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    discharge_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CurrentStatusEnum = table.Column<int>(type: "integer", nullable: false),
                    triage_notes = table.Column<string>(type: "text", nullable: true),
                    priority_level = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'white'::character varying"),
                    patient_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("emergency_visits_pkey", x => x.visit_id);
                    table.ForeignKey(
                        name: "emergency_visits_patient_id_fkey",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "patient_id");
                    table.ForeignKey(
                        name: "emergency_visits_priority_level_fkey",
                        column: x => x.priority_level,
                        principalTable: "priority_levels",
                        principalColumn: "priority_code");
                });

            migrationBuilder.CreateTable(
                name: "EmergencyVisitStaffs",
                columns: table => new
                {
                    StaffRoleEnum = table.Column<int>(type: "integer", nullable: false),
                    visit_id = table.Column<int>(type: "integer", nullable: false),
                    staff_id = table.Column<int>(type: "integer", nullable: false),
                    assigned_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("emergency_visit_staff_pkey", x => new { x.visit_id, x.staff_id, x.StaffRoleEnum });
                    table.ForeignKey(
                        name: "emergency_visit_staff_staff_id_fkey",
                        column: x => x.staff_id,
                        principalTable: "staff",
                        principalColumn: "staff_id");
                    table.ForeignKey(
                        name: "emergency_visit_staff_visit_id_fkey",
                        column: x => x.visit_id,
                        principalTable: "emergency_visits",
                        principalColumn: "visit_id");
                });

            migrationBuilder.CreateTable(
                name: "hospital_beds",
                columns: table => new
                {
                    bed_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    current_visit_id = table.Column<int>(type: "integer", nullable: true),
                    ward_section = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    bed_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BedStatusEnum = table.Column<int>(type: "integer", nullable: false),
                    last_cleaned_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("hospital_beds_pkey", x => x.bed_id);
                    table.ForeignKey(
                        name: "hospital_beds_current_visit_id_fkey",
                        column: x => x.current_visit_id,
                        principalTable: "emergency_visits",
                        principalColumn: "visit_id");
                });

            migrationBuilder.CreateTable(
                name: "medical_procedures",
                columns: table => new
                {
                    procedure_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    visit_id = table.Column<int>(type: "integer", nullable: false),
                    performed_by_staff_id = table.Column<int>(type: "integer", nullable: false),
                    procedure_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    procedure_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    description = table.Column<string>(type: "text", nullable: true),
                    procedure_cost = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("medical_procedures_pkey", x => x.procedure_id);
                    table.ForeignKey(
                        name: "medical_procedures_performed_by_staff_id_fkey",
                        column: x => x.performed_by_staff_id,
                        principalTable: "staff",
                        principalColumn: "staff_id");
                    table.ForeignKey(
                        name: "medical_procedures_visit_id_fkey",
                        column: x => x.visit_id,
                        principalTable: "emergency_visits",
                        principalColumn: "visit_id");
                });

            migrationBuilder.CreateTable(
                name: "patient_invoices",
                columns: table => new
                {
                    invoice_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    visit_id = table.Column<int>(type: "integer", nullable: false),
                    created_by_staff_id = table.Column<int>(type: "integer", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    invoice_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    payment_status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, defaultValueSql: "'pending'::character varying"),
                    payment_received_amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, defaultValueSql: "0"),
                    payment_received_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("patient_invoices_pkey", x => x.invoice_id);
                    table.ForeignKey(
                        name: "patient_invoices_created_by_staff_id_fkey",
                        column: x => x.created_by_staff_id,
                        principalTable: "staff",
                        principalColumn: "staff_id");
                    table.ForeignKey(
                        name: "patient_invoices_visit_id_fkey",
                        column: x => x.visit_id,
                        principalTable: "emergency_visits",
                        principalColumn: "visit_id");
                });

            migrationBuilder.CreateTable(
                name: "patient_vitals",
                columns: table => new
                {
                    vital_record_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    visit_id = table.Column<int>(type: "integer", nullable: false),
                    recorded_by_staff_id = table.Column<int>(type: "integer", nullable: false),
                    recorded_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    body_temperature = table.Column<decimal>(type: "numeric(4,1)", precision: 4, scale: 1, nullable: true),
                    blood_pressure_systolic = table.Column<int>(type: "integer", nullable: true),
                    blood_pressure_diastolic = table.Column<int>(type: "integer", nullable: true),
                    heart_rate = table.Column<int>(type: "integer", nullable: true),
                    respiratory_rate = table.Column<int>(type: "integer", nullable: true),
                    oxygen_saturation = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    additional_observations = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("patient_vitals_pkey", x => x.vital_record_id);
                    table.ForeignKey(
                        name: "patient_vitals_recorded_by_staff_id_fkey",
                        column: x => x.recorded_by_staff_id,
                        principalTable: "staff",
                        principalColumn: "staff_id");
                    table.ForeignKey(
                        name: "patient_vitals_visit_id_fkey",
                        column: x => x.visit_id,
                        principalTable: "emergency_visits",
                        principalColumn: "visit_id");
                });

            migrationBuilder.InsertData(
                table: "hospital_beds",
                columns: new[] { "bed_id", "bed_number", "BedStatusEnum", "current_visit_id", "last_cleaned_timestamp", "ward_section" },
                values: new object[] { 1, "1", 0, null, null, "PRIORITY" });

            migrationBuilder.InsertData(
                table: "patients",
                columns: new[] { "patient_id", "address", "contact_number", "date_of_birth", "email", "emergency_contact_name", "emergency_contact_number", "first_name", "gender", "insurance_policy_number", "insurance_provider", "last_name" },
                values: new object[,]
                {
                    { 1, "1234 Main St", null, new DateOnly(1, 1, 1), null, null, null, "Pepe", null, "12345", null, "Pizza" },
                    { 2, "1000 Main St", null, new DateOnly(1, 1, 1), null, null, null, "Tom", null, "12346", null, "Fredy" },
                    { 3, "5000 Main St", null, new DateOnly(1, 1, 1), null, null, null, "Sofia", null, "12347", null, "Falcony" },
                    { 4, "3333 Haf Baf", null, new DateOnly(1, 1, 1), null, null, null, "Labra", null, "12345", null, "Dor" }
                });

            migrationBuilder.InsertData(
                table: "staff",
                columns: new[] { "staff_id", "department", "email", "first_name", "last_name", "phone_number", "RoleEnum", "specialization" },
                values: new object[] { 1, null, "contact@email.com", "Doctor", "1", null, 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyVisitStaffs_staff_id",
                table: "EmergencyVisitStaffs",
                column: "staff_id");

            migrationBuilder.CreateIndex(
                name: "idx_emergency_visit_staff",
                table: "EmergencyVisitStaffs",
                columns: new[] { "visit_id", "staff_id" });

            migrationBuilder.CreateIndex(
                name: "idx_patient_visits",
                table: "emergency_visits",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "idx_visit_priority",
                table: "emergency_visits",
                column: "priority_level");

            migrationBuilder.CreateIndex(
                name: "IX_hospital_beds_current_visit_id",
                table: "hospital_beds",
                column: "current_visit_id");

            migrationBuilder.CreateIndex(
                name: "hospital_beds_ward_section_bed_number_key",
                table: "hospital_beds",
                columns: new[] { "ward_section", "bed_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_medical_procedures_performed_by_staff_id",
                table: "medical_procedures",
                column: "performed_by_staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_medical_procedures_visit_id",
                table: "medical_procedures",
                column: "visit_id");

            migrationBuilder.CreateIndex(
                name: "IX_patient_invoices_created_by_staff_id",
                table: "patient_invoices",
                column: "created_by_staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_patient_invoices_visit_id",
                table: "patient_invoices",
                column: "visit_id");

            migrationBuilder.CreateIndex(
                name: "IX_patient_vitals_recorded_by_staff_id",
                table: "patient_vitals",
                column: "recorded_by_staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_patient_vitals_visit_id",
                table: "patient_vitals",
                column: "visit_id");

            migrationBuilder.CreateIndex(
                name: "idx_patient_name",
                table: "patients",
                columns: new[] { "last_name", "first_name" });

            migrationBuilder.CreateIndex(
                name: "staff_email_key",
                table: "staff",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmergencyVisitStaffs");

            migrationBuilder.DropTable(
                name: "hospital_beds");

            migrationBuilder.DropTable(
                name: "medical_procedures");

            migrationBuilder.DropTable(
                name: "patient_invoices");

            migrationBuilder.DropTable(
                name: "patient_vitals");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "emergency_visits");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "priority_levels");
        }
    }
}
