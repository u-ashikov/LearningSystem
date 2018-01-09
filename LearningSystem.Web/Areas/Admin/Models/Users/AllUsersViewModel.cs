namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    using Services.Models.User;
    using System.Collections.Generic;
    using Web.Models;

    public class AllUsersViewModel
    {
		public IEnumerable<UserListingServiceModel> Users { get; set; }

		public PaginationViewModel Pagination { get; set; }
    }
}
