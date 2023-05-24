using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.Migrations
{
    public partial class EmployerDetailsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "Employer",
                newName: "TradingName");

            migrationBuilder.AlterColumn<string>(
                name: "CareerObjective",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Employer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApproverNote",
                table: "Employer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "Employer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyRegistrationNumber",
                table: "Employer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Employer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Employer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isApproved",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "ApproverNote",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "CompanyRegistrationNumber",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "isApproved",
                table: "Employer");

            migrationBuilder.RenameColumn(
                name: "TradingName",
                table: "Employer",
                newName: "RegistrationNumber");

            migrationBuilder.AlterColumn<string>(
                name: "CareerObjective",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
