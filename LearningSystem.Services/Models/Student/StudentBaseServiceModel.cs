namespace LearningSystem.Services.Models.Student
{
    using AutoMapper;
    using Data.Enums;
    using Data.Models;
    using Infrastructure.Automapper;
    using System.Linq;

    public class StudentBaseServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
		public string Id { get; set; }

		public string Username { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public Grade? Grade { get; set; }

		public bool HasSolution { get; set; }

		public void ConfigureMapping(Profile mapper)
		{
			int courseId = 0;
			string trainerId = null;

			mapper.CreateMap<User, StudentBaseServiceModel>()
				.ForMember(dest => dest.Grade, cfg => cfg.MapFrom(src => src.Courses
					.Where(c => c.CourseId == courseId && c.Course.TrainerId == trainerId)
					.Select(c => c.Grade).FirstOrDefault()))
				.ForMember(dest => dest.HasSolution, cfg => cfg.MapFrom(src => src.Courses
					.Where(c => c.CourseId == courseId && c.Course.TrainerId == trainerId)
					.Any(c=>c.Exam != null)));
		}
	}
}
