using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeCheckApp.Migrations
{
    public partial class IDInGradesAndForeignKeyInPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Grades",
               columns: table => new
               {
                   ID = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   GradeCode = table.Column<string>(nullable: true),
                   GradeName = table.Column<string>(nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Grades", x => x.ID);
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
                   Country = table.Column<string>(nullable: true),
                   GradeID = table.Column<int>(nullable: false)
               },

               constraints: table =>
               {
                   table.PrimaryKey("PK_Persons", x => x.ID);
                   table.ForeignKey(
                       name: "FK_Persons_Grades_GradeID",
                       column: x => x.GradeID,
                       principalTable: "Grades",
                       principalColumn: "ID",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
              name: "IX_Persons_GradeID",
              table: "Persons",
              column: "GradeID",
              unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
