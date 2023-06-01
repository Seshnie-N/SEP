using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.Migrations
{
    public partial class ProfileBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qualification_Student_StudentUserId",
                table: "Qualification");

            migrationBuilder.DropForeignKey(
                name: "FK_Referee_Student_StudentUserId",
                table: "Referee");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperience_Student_StudentUserId",
                table: "WorkExperience");

            migrationBuilder.DropIndex(
                name: "IX_WorkExperience_StudentUserId",
                table: "WorkExperience");

            migrationBuilder.DropIndex(
                name: "IX_Referee_StudentUserId",
                table: "Referee");

            migrationBuilder.DropIndex(
                name: "IX_Qualification_StudentUserId",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "WorkExperience");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "Referee");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "Qualification");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "WorkExperience",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Referee",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Qualification",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperience_StudentId",
                table: "WorkExperience",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Referee_StudentId",
                table: "Referee",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_StudentId",
                table: "Qualification",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualification_Student_StudentId",
                table: "Qualification",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Referee_Student_StudentId",
                table: "Referee",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperience_Student_StudentId",
                table: "WorkExperience",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qualification_Student_StudentId",
                table: "Qualification");

            migrationBuilder.DropForeignKey(
                name: "FK_Referee_Student_StudentId",
                table: "Referee");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperience_Student_StudentId",
                table: "WorkExperience");

            migrationBuilder.DropIndex(
                name: "IX_WorkExperience_StudentId",
                table: "WorkExperience");

            migrationBuilder.DropIndex(
                name: "IX_Referee_StudentId",
                table: "Referee");

            migrationBuilder.DropIndex(
                name: "IX_Qualification_StudentId",
                table: "Qualification");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "WorkExperience",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "StudentUserId",
                table: "WorkExperience",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Referee",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "StudentUserId",
                table: "Referee",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Qualification",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "StudentUserId",
                table: "Qualification",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperience_StudentUserId",
                table: "WorkExperience",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Referee_StudentUserId",
                table: "Referee",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_StudentUserId",
                table: "Qualification",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualification_Student_StudentUserId",
                table: "Qualification",
                column: "StudentUserId",
                principalTable: "Student",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Referee_Student_StudentUserId",
                table: "Referee",
                column: "StudentUserId",
                principalTable: "Student",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperience_Student_StudentUserId",
                table: "WorkExperience",
                column: "StudentUserId",
                principalTable: "Student",
                principalColumn: "UserId");
        }
    }
}
