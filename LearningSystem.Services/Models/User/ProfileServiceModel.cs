namespace LearningSystem.Services.Models.User
{
	using System.Collections.Generic;

	public class ProfileServiceModel : ProfileBaseServiceModel
    {
		public IEnumerable<string> Roles { get; set; }
    }
}
