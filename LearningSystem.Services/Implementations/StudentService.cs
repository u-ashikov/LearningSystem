namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Models;
    using LearningSystem.Data.Enums;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Models.Student;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Data;

    public class StudentService : IStudentService
    {
		private readonly LearningSystemDbContext db;

		public StudentService(LearningSystemDbContext db)
		{
			this.db = db;
		}

		public bool IsInCourse(string studentId, int courseId)
		{
			return this.db.Courses
				.Include(c => c.Students)
				.FirstOrDefault(c=>c.Id == courseId)			
				.Students
				.Any(s => s.StudentId == studentId);
		}

		public void SignUpForCourse(string studentId, int courseId)
		{
			var student = this.db.Users.Find(studentId);

			student.Courses.Add(new StudentCourse()
			{
				CourseId = courseId
			});

			this.db.SaveChanges();
		}

		public void SignOutFromCourse(string studentId, int courseId)
		{
			var student = this.db.Users.Find(studentId);

			var courseToRemove = this.db.StudentCourses.FirstOrDefault(c=>c.CourseId == courseId && c.StudentId == studentId);

			student.Courses.Remove(courseToRemove);

			this.db.SaveChanges();
		}

		public StudentCoursesServiceModel GetStudentCourses(string id)
		{
			return this.db.Users
				.Where(u => u.Id == id)
				.ProjectTo<StudentCoursesServiceModel>(new { studentId = id})
				.FirstOrDefault();
		}

		public async Task UploadSolution(int courseId, string studentId, IFormFile solution)
		{
			var studentCourse = this.db.StudentCourses.FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);

			using (var ms = new MemoryStream())
			{
				await solution.CopyToAsync(ms);
				studentCourse.Exam = ms.ToArray();
			}

			this.db.SaveChanges();
		}

		public StudentCourseCertificateServiceModel GetCourseCertificate(int courseId, string studentId) => this.db.StudentCourses
						.Where(sc => sc.StudentId == studentId && sc.CourseId == courseId && sc.Grade <= Grade.C)
						.ProjectTo<StudentCourseCertificateServiceModel>()
						.FirstOrDefault();
	}
}
