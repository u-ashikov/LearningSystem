namespace LearningSystem.Services.Models.Trainer
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Automapper;
    using Services.Models.Student;
    using System.Collections.Generic;
    using System.Linq;

    public class TrainerCourseStudentsListingModel : IMapFrom<Course>, IHaveCustomMapping
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string StartDate { get; set; }

		public string EndDate { get; set; }

		public IEnumerable<StudentBaseServiceModel> Students { get; set; }

		public void ConfigureMapping(Profile mapper)
		{
			mapper.CreateMap<Course, TrainerCourseStudentsListingModel>()
				.ForMember(dest => dest.StartDate, cfg => cfg.MapFrom(src => src.StartDate.ToShortDateString()))
				.ForMember(dest => dest.EndDate, cfg => cfg.MapFrom(src => src.EndDate.ToShortDateString()))
				.ForMember(dest => dest.Students, cfg => cfg.MapFrom(src => src.Students.Select(c=>c.Student)));
		}
	}
}
