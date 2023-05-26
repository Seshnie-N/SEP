using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.Migrations
{
    public partial class postF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Faculties_facultyId",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "facultyId",
                table: "Departments",
                newName: "FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_facultyId",
                table: "Departments",
                newName: "IX_Departments_FacultyId");

            migrationBuilder.AddColumn<string>(
                name: "facultyName",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "facultyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "facultyName",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "Departments",
                newName: "facultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                newName: "IX_Departments_facultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Faculties_facultyId",
                table: "Departments",
                column: "facultyId",
                principalTable: "Faculties",
                principalColumn: "facultyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
