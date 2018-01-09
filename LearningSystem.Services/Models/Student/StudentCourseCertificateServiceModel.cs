namespace LearningSystem.Services.Models.Student
{
    using AutoMapper;
    using Data.Enums;
    using Data.Models;
    using Infrastructure.Automapper;

    public class StudentCourseCertificateServiceModel : IMapFrom<StudentCourse>, IHaveCustomMapping
    {
		public string StudentName { get; set; }

		public string CourseName { get; set; }

		public string StartDate { get; set; }

		public string EndDate { get; set; }

		public string Trainer { get; set; }

		public Grade? Grade { get; set; }

		public void ConfigureMapping(Profile mapper)
		{
			mapper.CreateMap<StudentCourse, StudentCourseCertificateServiceModel>()
				.ForMember(dest => dest.StudentName, cfg => cfg.MapFrom(src => src.Student.Name))
				.ForMember(dest => dest.CourseName, cfg => cfg.MapFrom(src => src.Course.Name))
				.ForMember(dest => dest.StartDate, cfg => cfg.MapFrom(src => src.Course.StartDate.ToShortDateString()))
				.ForMember(dest => dest.EndDate, cfg => cfg.MapFrom(src => src.Course.EndDate.ToShortDateString()))
				.ForMember(dest => dest.Trainer, cfg => cfg.MapFrom(src => src.Course.Trainer.Name))
				.ForMember(dest => dest.Grade, cfg => cfg.MapFrom(src => src.Grade));
		}
	}
}
