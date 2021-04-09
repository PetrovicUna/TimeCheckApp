using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeCheckApp.Migrations
{
    public partial class AddTemporaryDataParemetersToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "PersonAbsences");

            //migrationBuilder.DropTable(
            //    name: "ProjectTasks");

            //migrationBuilder.DropTable(
            //    name: "WorkingHourses");

            //migrationBuilder.DropTable(
            //    name: "Absences");

            //migrationBuilder.DropTable(
            //    name: "Projects");

            //migrationBuilder.DropTable(
            //    name: "Persons");

            //migrationBuilder.DropTable(
            //    name: "Tasks");

            //migrationBuilder.DropTable(
            //    name: "Grades");

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Commnent",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Days",
                table: "TemporaryData",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FM",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradeCode",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradeName",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Hours",
                table: "TemporaryData",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "LMPersonNumber",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LineManagerName",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonNumber",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelocatedCountry",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskName",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskNumber",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeCardStatus",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeEntryStatus",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeType",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "TemporaryData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkLocation",
                table: "TemporaryData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "Client",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "Commnent",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "FM",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "GradeCode",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "GradeName",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "LMPersonNumber",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "LineManagerName",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "PersonNumber",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "RelocatedCountry",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "TaskName",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "TaskNumber",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "TimeCardStatus",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "TimeEntryStatus",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "TimeType",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "TemporaryData");

            migrationBuilder.DropColumn(
                name: "WorkLocation",
                table: "TemporaryData");

            //    migrationBuilder.CreateTable(
            //        name: "Absences",
            //        columns: table => new
            //        {
            //            ID = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Date = table.Column<DateTime>(type: "datetime2", nullable: false),
            //            Hours = table.Column<float>(type: "real", nullable: false),
            //            PersonID = table.Column<int>(type: "int", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Absences", x => x.ID);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Grades",
            //        columns: table => new
            //        {
            //            GradeCode = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            GradeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Grades", x => x.GradeCode);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Projects",
            //        columns: table => new
            //        {
            //            ID = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ProjectCode = table.Column<int>(type: "int", nullable: false),
            //            ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Projects", x => x.ID);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Tasks",
            //        columns: table => new
            //        {
            //            ID = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            FM = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            TaskNumber = table.Column<int>(type: "int", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Tasks", x => x.ID);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Persons",
            //        columns: table => new
            //        {
            //            ID = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            GradeCode = table.Column<int>(type: "int", nullable: false),
            //            Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            PersonNumber = table.Column<int>(type: "int", nullable: false),
            //            Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            WorkLocation = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Persons", x => x.ID);
            //            table.ForeignKey(
            //                name: "FK_Persons_Grades_GradeCode",
            //                column: x => x.GradeCode,
            //                principalTable: "Grades",
            //                principalColumn: "GradeCode",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "ProjectTasks",
            //        columns: table => new
            //        {
            //            ProjectID = table.Column<int>(type: "int", nullable: false),
            //            TaskID = table.Column<int>(type: "int", nullable: false),
            //            ID = table.Column<int>(type: "int", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ProjectTasks", x => new { x.ProjectID, x.TaskID });
            //            table.ForeignKey(
            //                name: "FK_ProjectTasks_Projects_ProjectID",
            //                column: x => x.ProjectID,
            //                principalTable: "Projects",
            //                principalColumn: "ID",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_ProjectTasks_Tasks_TaskID",
            //                column: x => x.TaskID,
            //                principalTable: "Tasks",
            //                principalColumn: "ID",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PersonAbsences",
            //        columns: table => new
            //        {
            //            AbsenceID = table.Column<int>(type: "int", nullable: false),
            //            PersonID = table.Column<int>(type: "int", nullable: false),
            //            ID = table.Column<int>(type: "int", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PersonAbsences", x => new { x.AbsenceID, x.PersonID });
            //            table.ForeignKey(
            //                name: "FK_PersonAbsences_Absences_AbsenceID",
            //                column: x => x.AbsenceID,
            //                principalTable: "Absences",
            //                principalColumn: "ID",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_PersonAbsences_Persons_PersonID",
            //                column: x => x.PersonID,
            //                principalTable: "Persons",
            //                principalColumn: "ID",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "WorkingHourses",
            //        columns: table => new
            //        {
            //            ID = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            Date = table.Column<DateTime>(type: "datetime2", nullable: false),
            //            Hours = table.Column<float>(type: "real", nullable: false),
            //            PersonID = table.Column<int>(type: "int", nullable: false),
            //            TaskID = table.Column<int>(type: "int", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_WorkingHourses", x => x.ID);
            //            table.ForeignKey(
            //                name: "FK_WorkingHourses_Persons_PersonID",
            //                column: x => x.PersonID,
            //                principalTable: "Persons",
            //                principalColumn: "ID",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_WorkingHourses_Tasks_TaskID",
            //                column: x => x.TaskID,
            //                principalTable: "Tasks",
            //                principalColumn: "ID",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PersonAbsences_PersonID",
            //        table: "PersonAbsences",
            //        column: "PersonID");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Persons_GradeCode",
            //        table: "Persons",
            //        column: "GradeCode",
            //        unique: true);

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ProjectTasks_TaskID",
            //        table: "ProjectTasks",
            //        column: "TaskID");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_WorkingHourses_PersonID",
            //        table: "WorkingHourses",
            //        column: "PersonID");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_WorkingHourses_TaskID",
            //        table: "WorkingHourses",
            //        column: "TaskID");
            //}
        }
    }
}
