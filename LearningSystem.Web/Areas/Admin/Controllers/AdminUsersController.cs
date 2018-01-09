namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Services.Contracts;
    using System.Threading.Tasks;
    using Web.Infrastructure.Enums;
    using Web.Models;

    using static Common.Constants.WebConstants;

    public class AdminUsersController : AdminController
    {
		private readonly IAdminUserService users;

		private readonly UserManager<User> userManager;

		public AdminUsersController(IAdminUserService users, UserManager<User> userManager)
		{
			this.users = users;
			this.userManager = userManager;
		}

		[Route(Routing.AdminAllUsers)]
		public IActionResult All(int page = 1)
        {
            if (page < MinPageSize)
            {
                return RedirectToAction(nameof(All), new { page = MinPageSize });
            }

            var pagination = new PaginationViewModel()
            {
                Action = nameof(All),
                PageSize = UsersPageSize,
                CurrentPage = page,
                TotalElements = this.users.TotalUsers()
            };

            if (page > pagination.TotalPages && pagination.TotalPages != 0)
            {
                return RedirectToAction(nameof(All), new { page = MinPageSize });
            }

            return View(new AllUsersViewModel()
            {
                Users = this.users.All(page, UsersPageSize),
                Pagination = pagination
            });
        }

		[Route(Routing.AdminAddUserToRole)]
		public async Task<IActionResult> AddToRole(string id)
		{
			var user = await this.users.GetUserById(id);

			if (user == null)
			{
				return NonExistentUser(id);
			}

			return View(new UserRolesFormModel()
			{
				Username = user.Username,
				Name = user.Name,
				UserRoles = user.UserRoles
			});
		}

		[HttpPost]
		[Route(Routing.AdminAddUserToRole)]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> AddToRole(string id, UserRolesFormModel user)
		{
			if (!this.users.UserExists(id))
			{
				return NonExistentUser(id);
			}

			var success = await this.users.AddToRole(id, user.Roles);

			if (success)
			{
				this.GenerateMessage(string.Format(SuccessAddedUserToRole, user.Name), Alert.Success);
			}

			return RedirectToAction(nameof(All));
		}

		private IActionResult NonExistentUser(string id)
		{
			this.GenerateMessage(string.Format(NonExistingUser, id), Alert.Warning);
			return RedirectToAction(nameof(All));
		}
	}
}
