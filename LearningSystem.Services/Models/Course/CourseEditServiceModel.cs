namespace LearningSystem.Services.Models.Course
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Automapper;
    using System;

    public class CourseEditServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string TrainerId { get; set; }

		public string Trainer { get; set; }

		public void ConfigureMapping(Profile profile)
		{
			profile.CreateMap<Course, CourseBaseServiceModel>()
				.ForMember(dest => dest.Trainer, cfg => cfg.MapFrom(src => src.Trainer.Name));
		}
	}
}
