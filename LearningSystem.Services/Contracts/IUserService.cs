namespace LearningSystem.Services.Contracts
{
    using Microsoft.AspNetCore.Identity;
    using Models.Course;
    using Models.User;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
		Task<ProfileServiceModel> GetUserProfileDetails(string id);

		ProfileBaseServiceModel GetProfileToEdit(string id);

		Task<IEnumerable<IdentityError>> Edit(string id, string username, string name, string email, DateTime birthdate, string newPassword, string oldPassword);

		bool UserExists(string id);

		Task<IEnumerable<TrainerServiceModel>> GetTrainers();

		string GetNameById(string id);

		IEnumerable<UserSearchServiceModel> SearchUsers(string username);
	}
}
