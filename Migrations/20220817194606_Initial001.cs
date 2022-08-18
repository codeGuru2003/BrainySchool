using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthRecordsPro.Migrations
{
    public partial class Initial001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    LoginHint = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicSchoolYearTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSchoolYearTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicSchoolYearTypes_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicSemesterTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSemesterTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicSemesterTypes_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassTypes_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacultyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyTypes_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMemberTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMemberTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyMemberTypes_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GenderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenderTypes_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallmentTypes_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaritalStatuses_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NationalityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalityTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NationalityTypes_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeriodTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodTypes_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schools_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentTypes_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicSchoolYears",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    AcademicSchoolTypeId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSchoolYears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicSchoolYears_AcademicSchoolYearTypes_AcademicSchoolTypeId",
                        column: x => x.AcademicSchoolTypeId,
                        principalTable: "AcademicSchoolYearTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicSchoolYears_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    ClassTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_ClassTypes_ClassTypeId",
                        column: x => x.ClassTypeId,
                        principalTable: "ClassTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    FacultyTypeId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    GenderTypeId = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    MaritalStatusTypeId = table.Column<int>(nullable: true),
                    Firstname = table.Column<string>(nullable: false),
                    Middlename = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculties_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faculties_FacultyTypes_FacultyTypeId",
                        column: x => x.FacultyTypeId,
                        principalTable: "FacultyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faculties_GenderTypes_GenderTypeId",
                        column: x => x.GenderTypeId,
                        principalTable: "GenderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faculties_MaritalStatuses_MaritalStatusTypeId",
                        column: x => x.MaritalStatusTypeId,
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faculties_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Firstname = table.Column<string>(nullable: false),
                    Middlename = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: false),
                    DateofBirth = table.Column<DateTime>(nullable: false),
                    NationalityTypeId = table.Column<int>(nullable: false),
                    StudentTypeId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    GenderTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_GenderTypes_GenderTypeId",
                        column: x => x.GenderTypeId,
                        principalTable: "GenderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_NationalityTypes_NationalityTypeId",
                        column: x => x.NationalityTypeId,
                        principalTable: "NationalityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_StudentTypes_StudentTypeId",
                        column: x => x.StudentTypeId,
                        principalTable: "StudentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicSemesters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    AcademicSchoolYearId = table.Column<int>(nullable: false),
                    AcademicSemesterTypeId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSemesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicSemesters_AcademicSchoolYears_AcademicSchoolYearId",
                        column: x => x.AcademicSchoolYearId,
                        principalTable: "AcademicSchoolYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicSemesters_AcademicSemesterTypes_AcademicSemesterTypeId",
                        column: x => x.AcademicSemesterTypeId,
                        principalTable: "AcademicSemesterTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicSemesters_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassBillingItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    AmountInUSD = table.Column<double>(nullable: false),
                    AmountInLRD = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassBillingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassBillingItems_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassBillingItems_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassInstallment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    InstallmentTypeId = table.Column<int>(nullable: false),
                    AmountinUSD = table.Column<double>(nullable: false),
                    AmountinLRD = table.Column<double>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassInstallment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassInstallment_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassInstallment_InstallmentTypes_InstallmentTypeId",
                        column: x => x.InstallmentTypeId,
                        principalTable: "InstallmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassInstallment_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassFaculties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    FacultyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassFaculties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassFaculties_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassFaculties_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassFaculties_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    GenderTypeId = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    FamilyMemberTypeId = table.Column<int>(nullable: false),
                    MaritalStatusTypeId = table.Column<int>(nullable: true),
                    Firstname = table.Column<string>(nullable: false),
                    Middlename = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_FamilyMemberTypes_FamilyMemberTypeId",
                        column: x => x.FamilyMemberTypeId,
                        principalTable: "FamilyMemberTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_GenderTypes_GenderTypeId",
                        column: x => x.GenderTypeId,
                        principalTable: "GenderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_MaritalStatuses_MaritalStatusTypeId",
                        column: x => x.MaritalStatusTypeId,
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    AcademicSemesterId = table.Column<int>(nullable: false),
                    AcademicSchoolYearId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentClasses_AcademicSchoolYears_AcademicSchoolYearId",
                        column: x => x.AcademicSchoolYearId,
                        principalTable: "AcademicSchoolYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentClasses_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentClasses_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentClasses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicSemesterPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    PeriodTypeId = table.Column<int>(nullable: false),
                    AcademicSemesterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSemesterPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicSemesterPeriods_AcademicSemesters_AcademicSemesterId",
                        column: x => x.AcademicSemesterId,
                        principalTable: "AcademicSemesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicSemesterPeriods_PeriodTypes_PeriodTypeId",
                        column: x => x.PeriodTypeId,
                        principalTable: "PeriodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicSemesterPeriods_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentGrades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentClassId = table.Column<int>(nullable: false),
                    AcademicSemesterPeriodId = table.Column<int>(nullable: false),
                    Grade = table.Column<double>(nullable: false),
                    GradeLetter = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGrades_AcademicSemesterPeriods_AcademicSemesterPeriodId",
                        column: x => x.AcademicSemesterPeriodId,
                        principalTable: "AcademicSemesterPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentGrades_StudentClasses_StudentClassId",
                        column: x => x.StudentClassId,
                        principalTable: "StudentClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSchoolYears_AcademicSchoolTypeId",
                table: "AcademicSchoolYears",
                column: "AcademicSchoolTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSchoolYears_RecordedBy",
                table: "AcademicSchoolYears",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSchoolYearTypes_RecordedBy",
                table: "AcademicSchoolYearTypes",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemesterPeriods_AcademicSemesterId",
                table: "AcademicSemesterPeriods",
                column: "AcademicSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemesterPeriods_PeriodTypeId",
                table: "AcademicSemesterPeriods",
                column: "PeriodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemesterPeriods_RecordedBy",
                table: "AcademicSemesterPeriods",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemesters_AcademicSchoolYearId",
                table: "AcademicSemesters",
                column: "AcademicSchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemesters_AcademicSemesterTypeId",
                table: "AcademicSemesters",
                column: "AcademicSemesterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemesters_RecordedBy",
                table: "AcademicSemesters",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemesterTypes_RecordedBy",
                table: "AcademicSemesterTypes",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClassBillingItems_ClassId",
                table: "ClassBillingItems",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassBillingItems_RecordedBy",
                table: "ClassBillingItems",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassTypeId",
                table: "Classes",
                column: "ClassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_RecordedBy",
                table: "Classes",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ClassFaculties_ClassId",
                table: "ClassFaculties",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassFaculties_FacultyId",
                table: "ClassFaculties",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassFaculties_RecordedBy",
                table: "ClassFaculties",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ClassInstallment_ClassId",
                table: "ClassInstallment",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassInstallment_InstallmentTypeId",
                table: "ClassInstallment",
                column: "InstallmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassInstallment_RecordedBy",
                table: "ClassInstallment",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTypes_RecordedBy",
                table: "ClassTypes",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_RecordedBy",
                table: "Currencies",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_ApplicationUserId",
                table: "Faculties",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_FacultyTypeId",
                table: "Faculties",
                column: "FacultyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_GenderTypeId",
                table: "Faculties",
                column: "GenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_MaritalStatusTypeId",
                table: "Faculties",
                column: "MaritalStatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_RecordedBy",
                table: "Faculties",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyTypes_RecordedBy",
                table: "FacultyTypes",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_FamilyMemberTypeId",
                table: "FamilyMembers",
                column: "FamilyMemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_GenderTypeId",
                table: "FamilyMembers",
                column: "GenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_MaritalStatusTypeId",
                table: "FamilyMembers",
                column: "MaritalStatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_RecordedBy",
                table: "FamilyMembers",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_StudentId",
                table: "FamilyMembers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMemberTypes_RecordedBy",
                table: "FamilyMemberTypes",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GenderTypes_RecordedBy",
                table: "GenderTypes",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Images_RecordedBy",
                table: "Images",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentTypes_RecordedBy",
                table: "InstallmentTypes",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_RecordedBy",
                table: "MaritalStatuses",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NationalityTypes_RecordedBy",
                table: "NationalityTypes",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodTypes_RecordedBy",
                table: "PeriodTypes",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_RecordedBy",
                table: "Schools",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_AcademicSchoolYearId",
                table: "StudentClasses",
                column: "AcademicSchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_ClassId",
                table: "StudentClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_RecordedBy",
                table: "StudentClasses",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_StudentId",
                table: "StudentClasses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrades_AcademicSemesterPeriodId",
                table: "StudentGrades",
                column: "AcademicSemesterPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrades_StudentClassId",
                table: "StudentGrades",
                column: "StudentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ApplicationUserId",
                table: "Students",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GenderTypeId",
                table: "Students",
                column: "GenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_NationalityTypeId",
                table: "Students",
                column: "NationalityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_RecordedBy",
                table: "Students",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentTypeId",
                table: "Students",
                column: "StudentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTypes_RecordedBy",
                table: "StudentTypes",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_RecordedBy",
                table: "Subjects",
                column: "RecordedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClassBillingItems");

            migrationBuilder.DropTable(
                name: "ClassFaculties");

            migrationBuilder.DropTable(
                name: "ClassInstallment");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "FamilyMembers");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "StudentGrades");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "InstallmentTypes");

            migrationBuilder.DropTable(
                name: "FamilyMemberTypes");

            migrationBuilder.DropTable(
                name: "AcademicSemesterPeriods");

            migrationBuilder.DropTable(
                name: "StudentClasses");

            migrationBuilder.DropTable(
                name: "FacultyTypes");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "AcademicSemesters");

            migrationBuilder.DropTable(
                name: "PeriodTypes");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "AcademicSchoolYears");

            migrationBuilder.DropTable(
                name: "AcademicSemesterTypes");

            migrationBuilder.DropTable(
                name: "ClassTypes");

            migrationBuilder.DropTable(
                name: "GenderTypes");

            migrationBuilder.DropTable(
                name: "NationalityTypes");

            migrationBuilder.DropTable(
                name: "StudentTypes");

            migrationBuilder.DropTable(
                name: "AcademicSchoolYearTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
