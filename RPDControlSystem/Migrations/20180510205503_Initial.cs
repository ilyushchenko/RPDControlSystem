using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RPDControlSystem.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Direction",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Qualification = table.Column<int>(nullable: false),
                    QualificationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direction", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BaseName = table.Column<string>(nullable: false),
                    Directory = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DirectionCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competence_Direction_DirectionCode",
                        column: x => x.DirectionCode,
                        principalTable: "Direction",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    DirectionCode = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Profile_Direction_DirectionCode",
                        column: x => x.DirectionCode,
                        principalTable: "Direction",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    EducationForm = table.Column<int>(nullable: false),
                    EducationFormId = table.Column<int>(nullable: false),
                    ProfileCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Plan_Profile_ProfileCode",
                        column: x => x.ProfileCode,
                        principalTable: "Profile",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileCompetence",
                columns: table => new
                {
                    ProfileCode = table.Column<string>(nullable: false),
                    CompetenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileCompetence", x => new { x.ProfileCode, x.CompetenceId });
                    table.ForeignKey(
                        name: "FK_ProfileCompetence_Competence_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileCompetence_Profile_ProfileCode",
                        column: x => x.ProfileCode,
                        principalTable: "Profile",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisciplineCode = table.Column<string>(nullable: false),
                    DisciplineType = table.Column<int>(nullable: false),
                    PlanCode = table.Column<string>(nullable: false),
                    WorkPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineInfo_Discipline_DisciplineCode",
                        column: x => x.DisciplineCode,
                        principalTable: "Discipline",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineInfo_Plan_PlanCode",
                        column: x => x.PlanCode,
                        principalTable: "Plan",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineInfo_File_WorkPlanId",
                        column: x => x.WorkPlanId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineCompetence",
                columns: table => new
                {
                    DisciplineInfoId = table.Column<int>(nullable: false),
                    CompetenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineCompetence", x => new { x.DisciplineInfoId, x.CompetenceId });
                    table.ForeignKey(
                        name: "FK_DisciplineCompetence_Competence_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineCompetence_DisciplineInfo_DisciplineInfoId",
                        column: x => x.DisciplineInfoId,
                        principalTable: "DisciplineInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competence_DirectionCode",
                table: "Competence",
                column: "DirectionCode");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineCompetence_CompetenceId",
                table: "DisciplineCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineInfo_DisciplineCode",
                table: "DisciplineInfo",
                column: "DisciplineCode");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineInfo_PlanCode",
                table: "DisciplineInfo",
                column: "PlanCode");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineInfo_WorkPlanId",
                table: "DisciplineInfo",
                column: "WorkPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_ProfileCode",
                table: "Plan",
                column: "ProfileCode");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_DirectionCode",
                table: "Profile",
                column: "DirectionCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCompetence_CompetenceId",
                table: "ProfileCompetence",
                column: "CompetenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplineCompetence");

            migrationBuilder.DropTable(
                name: "ProfileCompetence");

            migrationBuilder.DropTable(
                name: "DisciplineInfo");

            migrationBuilder.DropTable(
                name: "Competence");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Direction");
        }
    }
}
