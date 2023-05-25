using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.Migrations
{
    public partial class PostsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "faculties",
                columns: table => new
                {
                    facultyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    facultyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faculties", x => x.facultyId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    postId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jobLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resposibilities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    partTimeHour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    hourlyRate = table.Column<int>(type: "int", nullable: false),
                    limitedToLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    limitedToCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    minimumRequirment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    applicationInstruction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    applicationClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    conatctPersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conatctPersonEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conatctPersonNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postreviewComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    departmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.postId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    departmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    departmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    facultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.departmentId);
                    table.ForeignKey(
                        name: "FK_Departments_faculties_facultyId",
                        column: x => x.facultyId,
                        principalTable: "faculties",
                        principalColumn: "facultyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_facultyId",
                table: "Departments",
                column: "facultyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "faculties");
        }
    }
}
