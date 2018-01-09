namespace LearningSystem.Services.Models.Course
{
    using Data.Models;
    using Infrastructure.Automapper;

    public class TrainerServiceModel : IMapFrom<User>
    {
		public string Id { get; set; }

		public string Name { get; set; }
    }
}
