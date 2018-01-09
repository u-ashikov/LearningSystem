namespace LearningSystem.Services.Implementations
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Enums;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Models.Course;
    using Models.User;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Data;

    using static Common.Constants.WebConstants;

    public class UserService : IUserService
	{
       private readonly LearningSystemDbContext db;

		private readonly UserManager<User> userManager;

		public UserService(LearningSystemDbContext db, UserManager<User> userManager)
		{
			this.db = db;
			this.userManager = userManager;
		}

		public bool UserExists(string id) => this.db.Users.Any(u => u.Id == id);

		public async Task<IEnumerable<TrainerServiceModel>> GetTrainers()
		{
			var usersInTrainerRole = await this.userManager.GetUsersInRoleAsync(Role.Trainer.ToString());

			return Mapper
					.Map<IEnumerable<TrainerServiceModel>>(usersInTrainerRole)
					.ToList();
		}

		public async Task<ProfileServiceModel> GetUserProfileDetails(string id)
		{
			var user =  this.db.Users
				.Where(u => u.Id == id)
				.ProjectTo<ProfileServiceModel>()
				.FirstOrDefault();

			user.Roles = await this.userManager.GetRolesAsync(this.db.Users.Find(id));

			return user;
		}

		public ProfileBaseServiceModel GetProfileToEdit(string id) =>
			this.db.Users
				.Where(u => u.Id == id)
				.ProjectTo<ProfileBaseServiceModel>()
				.FirstOrDefault();

		public async Task<IEnumerable<IdentityError>> Edit(string id, string username, string name, string email, DateTime birthdate, string newPassword, string oldPassword)
		{
			var user = this.db.Users.Find(id);
			var errors = new HashSet<IdentityError>();

			if (string.IsNullOrEmpty(username))
			{
				errors.Add(new IdentityError()
				{
					Description = EmptyUsername
                });

				return errors;
			}

			user.UserName = username;

			if (string.IsNullOrEmpty(name))
			{
				errors.Add(new IdentityError()
				{
					Description = EmptyName
				});

				return errors;
			}

			user.Name = name;

			user.BirthDate = birthdate;
			this.db.SaveChanges();

			if (!string.IsNullOrEmpty(email))
			{
				var emailToken = await this.userManager.GenerateChangeEmailTokenAsync(user, email);
				var emailChanged = await this.userManager.ChangeEmailAsync(user, email, emailToken);

				if (!emailChanged.Succeeded)
				{
					foreach (var error in emailChanged.Errors)
					{
						errors.Add(error);
					}

					return errors;
				}
			}

			if (!string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(oldPassword))
			{
				bool oldPasswordMatch = await this.userManager.CheckPasswordAsync(user, oldPassword);

				if (!oldPasswordMatch)
				{
					errors.Add(new IdentityError()
					{
						Description = IncorrectOldPassword
                    });

					return errors;
				}

				var passwordChanged = await this.userManager.ChangePasswordAsync(user, oldPassword, newPassword);

				if (!passwordChanged.Succeeded)
				{
					foreach (var error in passwordChanged.Errors)
					{
						errors.Add(error);
					}

					return errors;
				}
			}

			return errors;
		}

		public string GetNameById(string id) => this.db.Users.FirstOrDefault(u => u.Id == id).Name;

		public IEnumerable<UserSearchServiceModel> SearchUsers(string username) =>
			this.db.Users
				.Where(u => u.UserName.ToLower().Contains(username.ToLower()))
				.ProjectTo<UserSearchServiceModel>()
				.ToList();
	}
}
