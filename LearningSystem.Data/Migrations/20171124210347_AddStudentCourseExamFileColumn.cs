namespace LearningSystem.Web.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddStudentCourseExamFileColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exam",
                table: "Solutions");

            migrationBuilder.AddColumn<byte[]>(
                name: "Exam",
                table: "StudentCourses",
                maxLength: 2000000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exam",
                table: "StudentCourses");

            migrationBuilder.AddColumn<byte[]>(
                name: "Exam",
                table: "Solutions",
                maxLength: 2000000,
                nullable: true);
        }
    }
}
