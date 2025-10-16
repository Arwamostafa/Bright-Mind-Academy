using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddColumsInStudentSubjectTableAndDropPaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectStudents_StudentProfiles_SubjectId",
                table: "SubjectStudents");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectStudents_StudentProfiles_StudentId",
                table: "SubjectStudents",
                column: "StudentId",
                principalTable: "StudentProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectStudents_StudentProfiles_StudentId",
                table: "SubjectStudents");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectStudents_StudentProfiles_SubjectId",
                table: "SubjectStudents",
                column: "SubjectId",
                principalTable: "StudentProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
