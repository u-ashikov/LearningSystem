namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Enums;
    using Models.Trainer;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Data;

    public class TrainerService : ITrainerService
	{
		private readonly LearningSystemDbContext db;

		public TrainerService(LearningSystemDbContext db)
		{
			this.db = db;
		}

		public IEnumerable<TrainerCourseListingServiceModel> TrainedCourses(string id, int page, int pageSize = 10)
		{
			return this.db.Courses
					.Where(c=>c.TrainerId == id)
					.ProjectTo<TrainerCourseListingServiceModel>()
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToList();
		}

		public TrainerCourseStudentsListingModel GetCourseStudentsForAssesment(int id, string trainerId)
		{
			return this.db.Courses
				.Where(c => c.Id == id && c.TrainerId == trainerId)
				.ProjectTo<TrainerCourseStudentsListingModel>(new { courseId = id , trainerId })
				.FirstOrDefault();
		}

		public byte[] GetStudentSolution(int courseId, string studentId) =>
			this.db.StudentCourses
				.FirstOrDefault(s => s.StudentId == studentId && s.CourseId == courseId)
				.Exam;

		public void AssesStudent(int courseId, string studentId, Grade grade)
		{
			var studentCourse = this.db.StudentCourses.FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);

			studentCourse.Grade = grade;

			this.db.SaveChanges();
		}

		public bool IsCourseTrainer(int courseId, string trainerId) =>
			this.db.Courses
				.Any(c => c.Id == courseId && c.TrainerId == trainerId);

		public int TrainerTotalCourses(string id) => this.db.Courses.Count(u => u.TrainerId == id);
	}
}
