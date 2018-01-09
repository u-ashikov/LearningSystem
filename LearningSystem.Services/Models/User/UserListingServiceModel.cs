namespace LearningSystem.Services.Models.User
{
	using Data.Models;
	using Infrastructure.Automapper;
	using System;

	public class UserListingServiceModel : IMapFrom<User>
    {
		public string Id { get; set; }

		public string Username { get; set; }

		public string Name { get; set; }

		public DateTime Birthdate { get; set; }

		public string Email { get; set; }
    }
}
