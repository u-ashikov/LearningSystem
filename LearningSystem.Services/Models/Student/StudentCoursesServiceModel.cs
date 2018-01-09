namespace LearningSystem.Services.Models.Student
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Automapper;
    using System.Collections.Generic;
    using System.Linq;

    public class StudentCoursesServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
		public string Username { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public IEnumerable<StudentCourseListingServiceModel> StudentCourses { get; set; }

		public void ConfigureMapping(Profile mapper)
		{
			mapper.CreateMap<User, StudentCoursesServiceModel>()
				.ForMember(dest => dest.StudentCourses, cfg => cfg.MapFrom(src => src.Courses.Select(c=>c.Course)));
		}
	}
}
