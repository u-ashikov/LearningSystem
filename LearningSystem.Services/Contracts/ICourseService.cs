namespace LearningSystem.Services.Contracts
{
    using Models.Course;
    using System.Collections.Generic;

    public interface ICourseService
    {
		IEnumerable<CourseListingServiceModel> All(int page, int pageSize = 10);

		CourseServiceModel GetCourseInfo(int id);

		CourseDetailsServiceModel GetCourseDetailsById(int id);

		bool CourseExist(int id);

		bool IsStudentInCourse(int courseId, string studentId);

		string GetCourseName(int courseId);

		int TotalCourses();

        bool CourseStarted(int courseId);

        bool CourseFinished(int courseId);

        bool IsCourseLastDay(int courseId);

		IEnumerable<CourseSearchServiceModel> SearchCourses(string name);
    }
}
