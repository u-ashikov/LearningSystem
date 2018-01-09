namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    using LearningSystem.Data.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.Constants.WebConstants;

    public class UserRolesFormModel
    {
		public string Name { get; set; }

		public string Username { get; set; }

		[Display(Name = Display.CurrentUserRoler)]
		public IList<string> UserRoles { get; set; }

		[Required]
		public IList<Role> Roles { get; set; }
    }
}
