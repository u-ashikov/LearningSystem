namespace LearningSystem.Services.Contracts
{
    using Models.Course;
    using System;

    public interface IAdminCourseService
    {
		void Add(string name, string description, string trainerId, DateTime startDate, DateTime endDate);

		void Edit(int id, string name, string description, string trainerId, DateTime startDate, DateTime endDate);

		CourseEditServiceModel GetCourseToEdit(int id);

		bool CourseExist(int id);
	}
}
