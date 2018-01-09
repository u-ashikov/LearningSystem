namespace LearningSystem.Services.Contracts
{
    using Data.Enums;
    using Models.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
		IEnumerable<UserListingServiceModel> All(int page, int pageSize = 10);

		Task<UserBaseServiceModel> GetUserById(string id);

		Task<bool> AddToRole(string id, IEnumerable<Role> roles);

		int TotalUsers();

		bool UserExists(string id);
	}
}
