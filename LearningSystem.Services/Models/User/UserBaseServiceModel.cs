namespace LearningSystem.Services.Models.User
{
    using Data.Models;
    using Infrastructure.Automapper;
    using System.Collections.Generic;

    public class UserBaseServiceModel : IMapFrom<User>
    {
		public string Username { get; set; }

		public string Name { get; set; }
		
		public IList<string> UserRoles { get; set; }
	}
}
