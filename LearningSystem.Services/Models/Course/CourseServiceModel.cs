namespace LearningSystem.Services.Models.Course
{
    using Data.Models;
    using Infrastructure.Automapper;
    using System;

    public class CourseServiceModel : IMapFrom<Course>
	{
		public string Name { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }
    }
}
