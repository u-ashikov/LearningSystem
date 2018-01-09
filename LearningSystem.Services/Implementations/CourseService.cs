namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Models.Course;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Data;

    public class CourseService : ICourseService
	{
		private readonly LearningSystemDbContext db;

		public CourseService(LearningSystemDbContext db)
		{
			this.db = db;
		}

		public IEnumerable<CourseListingServiceModel> All(int page, int pageSize = 10) =>
			this.db.Courses
				.Where(c => c.EndDate >= DateTime.Now)
				.OrderByDescending(c => c.Id)
				.Skip((page-1)*pageSize)
				.Take(pageSize)
				.ProjectTo<CourseListingServiceModel>();

		public CourseDetailsServiceModel GetCourseDetailsById(int id) =>
			this.db.Courses
				.Where(c => c.Id == id)
				.ProjectTo<CourseDetailsServiceModel>()
				.FirstOrDefault();

		public CourseServiceModel GetCourseInfo(int id) =>
			this.db.Courses
				.Where(c => c.Id == id)
				.ProjectTo<CourseServiceModel>()
				.FirstOrDefault();

		public bool CourseExist(int id) => this.db.Courses.Any(c => c.Id == id);

		public bool IsStudentInCourse(int courseId, string studentId) => this.db.StudentCourses.Any(sc => sc.CourseId == courseId && sc.StudentId == studentId);

		public string GetCourseName(int courseId) => this.db.Courses.Find(courseId).Name;

		public int TotalCourses() => this.db.Courses.Count(c=>c.EndDate >= DateTime.Now);

		public IEnumerable<CourseSearchServiceModel> SearchCourses(string name) => 
			this.db.Courses
			.Where(c => c.Name.ToLower().Contains(name.ToLower()))
			.ProjectTo<CourseSearchServiceModel>()
			.ToList();

        public bool CourseStarted(int courseId)
        {
            var course = this.db.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                return true;
            }

            return course.StartDate.Date <= DateTime.Now.Date;
        }

        public bool CourseFinished(int courseId)
        {
            var course = this.db.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                return true;
            }

            return course.EndDate.Date <= DateTime.Now.Date;
        }

        public bool IsCourseLastDay(int courseId)
        {
            var course = this.db.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                return true;
            }

            return course.EndDate.Date == DateTime.Now.Date;
        }
    }
}
