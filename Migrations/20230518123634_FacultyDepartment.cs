using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.Migrations
{
    public partial class FacultyDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_faculties_facultyId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_facultyId",
                table: "Departments");

            migrationBuilder.DeleteData(
                table: "faculties",
                keyColumn: "facultyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "faculties",
                keyColumn: "facultyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "faculties",
                keyColumn: "facultyId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "facultyId",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "departmentId",
                table: "faculties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_faculties_departmentId",
                table: "faculties",
                column: "departmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_faculties_Departments_departmentId",
                table: "faculties",
                column: "departmentId",
                principalTable: "Departments",
                principalColumn: "departmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_faculties_Departments_departmentId",
                table: "faculties");

            migrationBuilder.DropIndex(
                name: "IX_faculties_departmentId",
                table: "faculties");

            migrationBuilder.DropColumn(
                name: "departmentId",
                table: "faculties");

            migrationBuilder.AddColumn<int>(
                name: "facultyId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "faculties",
                columns: new[] { "facultyId", "facultyName" },
                values: new object[] { 1, "Commerce, Law and Management" });

            migrationBuilder.InsertData(
                table: "faculties",
                columns: new[] { "facultyId", "facultyName" },
                values: new object[] { 2, "Engineering and the Built Environment" });

            migrationBuilder.InsertData(
                table: "faculties",
                columns: new[] { "facultyId", "facultyName" },
                values: new object[] { 3, "Health Sciences" });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_facultyId",
                table: "Departments",
                column: "facultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_faculties_facultyId",
                table: "Departments",
                column: "facultyId",
                principalTable: "faculties",
                principalColumn: "facultyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
