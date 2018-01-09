namespace LearningSystem.Services.Models.Course
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Automapper;

    public class CourseListingServiceModel : CourseServiceModel, IMapFrom<Course>, IHaveCustomMapping
    {
		public int Id { get; set; }

		public string TrainerName { get; set; }

		public void ConfigureMapping(Profile profile)
		{
			profile.CreateMap<Course, CourseListingServiceModel>()
				.ForMember(dest => dest.TrainerName, cfg => cfg.MapFrom(src => src.Trainer.Name));
		}
	}
}
