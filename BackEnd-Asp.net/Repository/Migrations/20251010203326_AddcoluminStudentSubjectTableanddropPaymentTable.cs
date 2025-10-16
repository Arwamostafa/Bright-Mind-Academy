using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddcoluminStudentSubjectTableanddropPaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectStudents_StudentProfiles_StudentId",
                table: "SubjectStudents");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectStudents",
                table: "SubjectStudents");

            migrationBuilder.DropIndex(
                name: "IX_SubjectStudents_StudentId",
                table: "SubjectStudents");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "SubjectStudents",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "SubjectStudents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "SubjectStudents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "SubjectStudents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectStudents",
                table: "SubjectStudents",
                columns: new[] { "StudentId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStudents_SubjectId",
                table: "SubjectStudents",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectStudents_StudentProfiles_SubjectId",
                table: "SubjectStudents",
                column: "SubjectId",
                principalTable: "StudentProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectStudents_StudentProfiles_SubjectId",
                table: "SubjectStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectStudents",
                table: "SubjectStudents");

            migrationBuilder.DropIndex(
                name: "IX_SubjectStudents_SubjectId",
                table: "SubjectStudents");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SubjectStudents");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "SubjectStudents");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "SubjectStudents");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "SubjectStudents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectStudents",
                table: "SubjectStudents",
                columns: new[] { "SubjectId", "StudentId" });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPayMent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_StudentProfiles_StudentID",
                        column: x => x.StudentID,
                        principalTable: "StudentProfiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStudents_StudentId",
                table: "SubjectStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentID_SubjectID",
                table: "Payments",
                columns: new[] { "StudentID", "SubjectID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SubjectID",
                table: "Payments",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectStudents_StudentProfiles_StudentId",
                table: "SubjectStudents",
                column: "StudentId",
                principalTable: "StudentProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
