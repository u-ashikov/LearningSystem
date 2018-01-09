namespace LearningSystem.Web.Data.Migrations
{
	using Microsoft.EntityFrameworkCore.Migrations;

	public partial class AddStudentCourseGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "StudentCourses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentCourses");
        }
    }
}
