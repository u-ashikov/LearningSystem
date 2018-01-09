namespace LearningSystem.Services.Models.Student
{
    using AutoMapper;
    using Data.Enums;
    using Data.Models;
    using Infrastructure.Automapper;
    using System;
    using System.Linq;

    public class StudentCourseListingServiceModel : IMapFrom<Course>, IHaveCustomMapping
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Trainer { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public Grade? Grade { get; set; }

		public void ConfigureMapping(Profile mapper)
		{
			string studentId = null;

			mapper.CreateMap<Course, StudentCourseListingServiceModel>()
				.ForMember(dest => dest.Trainer, cfg => cfg.MapFrom(src => src.Trainer.Name))
				.ForMember(dest => dest.Grade, cfg => cfg.MapFrom(src => src.Students
					.Where(s => s.StudentId == studentId)
					.Select(s => s.Grade)
					.FirstOrDefault()));
		}
	}
}
