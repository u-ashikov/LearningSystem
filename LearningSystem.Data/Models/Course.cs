namespace LearningSystem.Data.Models
{
    using Common.Constants;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
		public int Id { get; set; }

		[Required]
		[StringLength(DataConstants.Course.NameMaxLength,MinimumLength = DataConstants.Course.NameMinLength,ErrorMessage = DataConstants.Error.NameLength)]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		public string TrainerId { get; set; }

		public User Trainer { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public List<StudentCourse> Students { get; set; } = new List<StudentCourse>();
    }
}
