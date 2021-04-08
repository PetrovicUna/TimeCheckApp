using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeCheckApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Absences",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(nullable: false),
                    Hours = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absences", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeCode);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCode = table.Column<int>(nullable: false),
                    ProjectName = table.Column<string>(nullable: true),
                    Client = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskNumber = table.Column<int>(nullable: false),
                    TaskName = table.Column<string>(nullable: true),
                    FM = table.Column<string>(nullable: true),
                    Activity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PersonNumber = table.Column<int>(nullable: false),
                    WorkLocation = table.Column<string>(nullable: true),
                    GradeCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Persons_Grades_GradeCode",
                        column: x => x.GradeCode,
                        principalTable: "Grades",
                        principalColumn: "GradeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTasks",
                columns: table => new
                {
                    ProjectID = table.Column<int>(nullable: false),
                    TaskID = table.Column<int>(nullable: false),
                    ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTasks", x => new { x.ProjectID, x.TaskID });
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonAbsences",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false),
                    AbsenceID = table.Column<int>(nullable: false),
                    ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAbsences", x => new { x.AbsenceID, x.PersonID });
                    table.ForeignKey(
                        name: "FK_PersonAbsences_Absences_AbsenceID",
                        column: x => x.AbsenceID,
                        principalTable: "Absences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonAbsences_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHourses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Hours = table.Column<float>(nullable: false),
                    PersonID = table.Column<int>(nullable: false),
                    TaskID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHourses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkingHourses_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingHourses_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonAbsences_PersonID",
                table: "PersonAbsences",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GradeCode",
                table: "Persons",
                column: "GradeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_TaskID",
                table: "ProjectTasks",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHourses_PersonID",
                table: "WorkingHourses",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHourses_TaskID",
                table: "WorkingHourses",
                column: "TaskID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonAbsences");

            migrationBuilder.DropTable(
                name: "ProjectTasks");

            migrationBuilder.DropTable(
                name: "TemporaryData");

            migrationBuilder.DropTable(
                name: "WorkingHourses");

            migrationBuilder.DropTable(
                name: "Absences");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Grades");
        }
    }
}
