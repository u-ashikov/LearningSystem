namespace LearningSystem.Services.Models.Course
{
    using Data.Models;
    using Infrastructure.Automapper;

    public class CourseSearchServiceModel : IMapFrom<Course>
    {
		public int Id { get; set; }

		public string Name { get; set; }
    }
}
