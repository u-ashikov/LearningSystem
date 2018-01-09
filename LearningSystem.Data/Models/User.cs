namespace LearningSystem.Data.Models
{
    using Common.Constants;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
		[Required]
		[StringLength(DataConstants.Student.NameMaxLength,MinimumLength = DataConstants.Student.NameMinLength,ErrorMessage = DataConstants.Error.NameLength)]
		public string Name { get; set; }

		public DateTime BirthDate { get; set; }

		public List<StudentCourse> Courses { get; set; } = new List<StudentCourse>();

		public List<Course> TrainedCourses { get; set; } = new List<Course>();

		public List<Article> Articles { get; set; } = new List<Article>();
    }
}
