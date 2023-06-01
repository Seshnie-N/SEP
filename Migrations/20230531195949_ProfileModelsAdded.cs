using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.Migrations
{
    public partial class ProfileModelsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployerName",
                table: "WorkExperience",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "WorkExperience",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "WorkExperience",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "WorkExperience",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TasksAndResponsibilities",
                table: "WorkExperience",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cell",
                table: "Referee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Referee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Institution",
                table: "Referee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Referee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Referee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Qualification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Institution",
                table: "Qualification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Majors",
                table: "Qualification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Research",
                table: "Qualification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Qualification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SubMajors",
                table: "Qualification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subjects",
                table: "Qualification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployerName",
                table: "WorkExperience");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "WorkExperience");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "WorkExperience");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "WorkExperience");

            migrationBuilder.DropColumn(
                name: "TasksAndResponsibilities",
                table: "WorkExperience");

            migrationBuilder.DropColumn(
                name: "Cell",
                table: "Referee");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Referee");

            migrationBuilder.DropColumn(
                name: "Institution",
                table: "Referee");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Referee");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Referee");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "Institution",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "Majors",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "Research",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "SubMajors",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "Subjects",
                table: "Qualification");
        }
    }
}
