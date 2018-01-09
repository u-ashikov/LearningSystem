namespace LearningSystem.Services.Models.User
{
    using Data.Models;
    using Infrastructure.Automapper;
    using System;

    public class ProfileBaseServiceModel : IMapFrom<User>
    {
		public string Username { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public DateTime BirthDate { get; set; }
	}
}
