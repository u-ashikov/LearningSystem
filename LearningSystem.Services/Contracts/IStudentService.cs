namespace LearningSystem.Services.Contracts
{
    using Microsoft.AspNetCore.Http;
    using Models.Student;
    using System.Threading.Tasks;

    public interface IStudentService
    {
		bool IsInCourse(string studentId, int courseId);

		void SignUpForCourse(string studentId, int courseId);

		void SignOutFromCourse(string studentId, int courseId);

		StudentCoursesServiceModel GetStudentCourses(string id);

		StudentCourseCertificateServiceModel GetCourseCertificate(int courseId, string studentId);

		Task UploadSolution(int courseId, string studentId, IFormFile solution);
	}
}
