namespace LearningSystem.Services.Models.Trainer
{
    using Data.Models;
    using Infrastructure.Automapper;
    using System;

    public class TrainerCourseListingServiceModel : IMapFrom<Course>
    {
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }
	}
}
