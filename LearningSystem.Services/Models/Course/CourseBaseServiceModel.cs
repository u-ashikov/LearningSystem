namespace LearningSystem.Services.Models.Course
{
	using System;
	using System.Collections.Generic;

	public class CourseBaseServiceModel
    {
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string TrainerId { get; set; }

		public string Trainer { get; set; }

		public IEnumerable<string> Students { get; set; }
	}
}
