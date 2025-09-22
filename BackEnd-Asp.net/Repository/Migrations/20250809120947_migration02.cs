using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class migration02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Students_StudentID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassSubjects_Instructors_InstructorID",
                table: "StudentClassSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Instructors_InstructorID",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectStudents_Students_StudentId",
                table: "SubjectStudents");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassSubjects_SubjectID",
                table: "StudentClassSubjects",
                column: "SubjectID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_StudentProfiles_StudentID",
                table: "Payments",
                column: "StudentID",
                principalTable: "StudentProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassSubjects_InstructorProfiles_InstructorID",
                table: "StudentClassSubjects",
                column: "InstructorID",
                principalTable: "InstructorProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_InstructorProfiles_InstructorID",
                table: "Subjects",
                column: "InstructorID",
                principalTable: "InstructorProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Payments_StudentProfiles_StudentID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassSubjects_InstructorProfiles_InstructorID",
                table: "StudentClassSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_InstructorProfiles_InstructorID",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectStudents_StudentProfiles_StudentId",
                table: "SubjectStudents");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassSubjects_SubjectID",
                table: "StudentClassSubjects");

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Students_StudentID",
                table: "Payments",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassSubjects_Instructors_InstructorID",
                table: "StudentClassSubjects",
                column: "InstructorID",
                principalTable: "Instructors",
                principalColumn: "InstructorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Instructors_InstructorID",
                table: "Subjects",
                column: "InstructorID",
                principalTable: "Instructors",
                principalColumn: "InstructorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectStudents_Students_StudentId",
                table: "SubjectStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
