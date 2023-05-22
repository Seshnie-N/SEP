using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.Migrations
{
    public partial class AddSomeFaculties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
