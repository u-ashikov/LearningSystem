namespace LearningSystem.Services.Contracts
{
    using Data.Enums;
    using Models.Trainer;
    using System.Collections.Generic;

    public interface ITrainerService
    {
		IEnumerable<TrainerCourseListingServiceModel> TrainedCourses(string id, int page, int pageSize = 10);

		TrainerCourseStudentsListingModel GetCourseStudentsForAssesment(int id, string trainerId);

		void AssesStudent(int courseId, string studentId, Grade grade);

		byte[] GetStudentSolution(int courseId, string studentId);

		bool IsCourseTrainer(int courseId, string trainerId);

		int TrainerTotalCourses(string id);
	}
}
