namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Course;
    using System;
    using System.Linq;
    using Web.Data;

    public class AdminCourseService : IAdminCourseService
    {
		private readonly LearningSystemDbContext db;

		public AdminCourseService(LearningSystemDbContext db)
		{
			this.db = db;
		}

		public void Add(string name, string description, string trainerId, DateTime startDate, DateTime endDate)
		{
			var course = new Course()
			{
				Name = name,
				Description = description,
				StartDate = startDate,
				EndDate = endDate,
				TrainerId = trainerId
			};

			this.db.Courses.Add(course);
			this.db.SaveChanges();
		}

		public void Edit(int id, string name, string description, string trainerId, DateTime startDate, DateTime endDate)
		{
			var course = this.db.Courses.FirstOrDefault(c => c.Id == id);

			course.Name = name;
			course.Description = description;
			course.StartDate = startDate;
			course.EndDate = endDate;
			course.TrainerId = trainerId;

			this.db.SaveChanges();
		}

		public CourseEditServiceModel GetCourseToEdit(int id) =>
			this.db.Courses
				.Include(c => c.Students)
				.Where(c => c.Id == id)
				.ProjectTo<CourseEditServiceModel>()
				.FirstOrDefault();

		public bool CourseExist(int id) => this.db.Courses.Any(c => c.Id == id);
	}
}
