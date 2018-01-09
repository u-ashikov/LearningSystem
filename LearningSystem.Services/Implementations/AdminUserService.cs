namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Common.Constants;
    using Contracts;
    using Data.Enums;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Models.User;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Data;

    public class AdminUserService : IAdminUserService
	{
		private readonly LearningSystemDbContext db;

		private readonly UserManager<User> userManager;

		public AdminUserService(LearningSystemDbContext db, UserManager<User> userManager)
		{
			this.db = db;
			this.userManager = userManager;
		}

		public IEnumerable<UserListingServiceModel> All(int page, int pageSize) =>
			this.db.Users
				.Where(u => u.UserName != AdminConstants.Username)
				.OrderBy(u => u.UserName)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ProjectTo<UserListingServiceModel>();

		public async Task<UserBaseServiceModel> GetUserById(string id)
		{
			var user = this.db.Users
				.Where(u => u.Id == id)
				.ProjectTo<UserBaseServiceModel>()
				.FirstOrDefault();

			user.UserRoles = await this.userManager.GetRolesAsync(this.db.Users.FirstOrDefault(u => u.Id == id));

			return user;
		}

		public async Task<bool> AddToRole(string id, IEnumerable<Role> roles)
		{
			var user = await this.userManager.FindByIdAsync(id);

			if (user != null)
			{
				foreach (var role in roles)
				{
					bool isInRole = await userManager.IsInRoleAsync(user, role.ToString());
					if (!isInRole)
					{
						await userManager.AddToRoleAsync(user, role.ToString());
					}
				}

				return true;
			}

			return false;
		}

		public int TotalUsers() => this.db.Users.Count();

		public bool UserExists(string id) => this.db.Users.Any(u => u.Id == id);
	}
}
