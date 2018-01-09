namespace LearningSystem.Services.Models.User
{
    using Data.Models;
    using Infrastructure.Automapper;

    public class UserSearchServiceModel : IMapFrom<User>
    {
		public string Id { get; set; }

		public string Username { get; set; }
    }
}
