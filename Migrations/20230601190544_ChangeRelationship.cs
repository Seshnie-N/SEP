using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.Migrations
{
    public partial class ChangeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkExperience",
                table: "WorkExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Referee",
                table: "Referee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Qualification",
                table: "Qualification");

            migrationBuilder.RenameTable(
                name: "WorkExperience",
                newName: "WorkExperiences");

            migrationBuilder.RenameTable(
                name: "Referee",
                newName: "Referees");

            migrationBuilder.RenameTable(
                name: "Qualification",
                newName: "Qualifications");

            migrationBuilder.RenameIndex(
                name: "IX_WorkExperience_StudentId",
                table: "WorkExperiences",
                newName: "IX_WorkExperiences_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Referee_StudentId",
                table: "Referees",
                newName: "IX_Referees_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Qualification_StudentId",
                table: "Qualifications",
                newName: "IX_Qualifications_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkExperiences",
                table: "WorkExperiences",
                column: "WorkExperienceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Referees",
                table: "Referees",
                column: "RefereeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Qualifications",
                table: "Qualifications",
                column: "QualificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualifications_Student_StudentId",
                table: "Qualifications",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Referees_Student_StudentId",
                table: "Referees",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperiences_Student_StudentId",
                table: "WorkExperiences",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qualifications_Student_StudentId",
                table: "Qualifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Referees_Student_StudentId",
                table: "Referees");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperiences_Student_StudentId",
                table: "WorkExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkExperiences",
                table: "WorkExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Referees",
                table: "Referees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Qualifications",
                table: "Qualifications");

            migrationBuilder.RenameTable(
                name: "WorkExperiences",
                newName: "WorkExperience");

            migrationBuilder.RenameTable(
                name: "Referees",
                newName: "Referee");

            migrationBuilder.RenameTable(
                name: "Qualifications",
                newName: "Qualification");

            migrationBuilder.RenameIndex(
                name: "IX_WorkExperiences_StudentId",
                table: "WorkExperience",
                newName: "IX_WorkExperience_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Referees_StudentId",
                table: "Referee",
                newName: "IX_Referee_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Qualifications_StudentId",
                table: "Qualification",
                newName: "IX_Qualification_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkExperience",
                table: "WorkExperience",
                column: "WorkExperienceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Referee",
                table: "Referee",
                column: "RefereeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Qualification",
                table: "Qualification",
                column: "QualificationId");

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
    }
}
