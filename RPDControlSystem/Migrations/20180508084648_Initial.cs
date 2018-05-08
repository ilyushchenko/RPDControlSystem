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
                name: "Competence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competence", x => x.Id);
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
                name: "EducationForm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationForm", x => x.Id);
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
                name: "Qualification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Direction",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    QualificationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direction", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Direction_Qualification_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectionCompetence",
                columns: table => new
                {
                    DirectionCode = table.Column<string>(nullable: false),
                    CompetenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionCompetence", x => new { x.DirectionCode, x.CompetenceId });
                    table.ForeignKey(
                        name: "FK_DirectionCompetence_Competence_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectionCompetence_Direction_DirectionCode",
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
                    EducationFormId = table.Column<int>(nullable: false),
                    ProfileCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Plan_EducationForm_EducationFormId",
                        column: x => x.EducationFormId,
                        principalTable: "EducationForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    PlanCode = table.Column<string>(nullable: false),
                    DisciplineCode = table.Column<string>(nullable: false),
                    WorkPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineInfo", x => new { x.PlanCode, x.DisciplineCode });
                    table.UniqueConstraint("AK_DisciplineInfo_DisciplineCode_PlanCode", x => new { x.DisciplineCode, x.PlanCode });
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
                    DisciplineCode = table.Column<string>(nullable: false),
                    CompetenceId = table.Column<int>(nullable: false),
                    PlanCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineCompetence", x => new { x.DisciplineCode, x.CompetenceId });
                    table.ForeignKey(
                        name: "FK_DisciplineCompetence_Competence_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineCompetence_DisciplineInfo_DisciplineCode_PlanCode",
                        columns: x => new { x.DisciplineCode, x.PlanCode },
                        principalTable: "DisciplineInfo",
                        principalColumns: new[] { "PlanCode", "DisciplineCode" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Direction_QualificationId",
                table: "Direction",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectionCompetence_CompetenceId",
                table: "DirectionCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineCompetence_CompetenceId",
                table: "DisciplineCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineCompetence_DisciplineCode_PlanCode",
                table: "DisciplineCompetence",
                columns: new[] { "DisciplineCode", "PlanCode" });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineInfo_WorkPlanId",
                table: "DisciplineInfo",
                column: "WorkPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_EducationFormId",
                table: "Plan",
                column: "EducationFormId");

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
                name: "DirectionCompetence");

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
                name: "EducationForm");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Direction");

            migrationBuilder.DropTable(
                name: "Qualification");
        }
    }
}
