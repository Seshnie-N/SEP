using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.Migrations
{
    public partial class addedBusinessTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessType",
                table: "Employer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessType",
                table: "Employer");
        }
    }
}
