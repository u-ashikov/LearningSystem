namespace LearningSystem.Web.Models.Account
{
    using Common.Constants;
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Common.Constants.WebConstants;

    public class EditProfileFormModel
    {
		[StringLength(DataConstants.Student.UsernameMaxLength, MinimumLength = DataConstants.Student.UsernameMinLength, ErrorMessage = DataConstants.Error.NameLength)]
		public string Username { get; set; }

		[StringLength(DataConstants.Student.NameMaxLength, MinimumLength = DataConstants.Student.NameMinLength, ErrorMessage = DataConstants.Error.NameLength)]
		public string Name { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime BirthDate { get; set; }

		[EmailAddress]
		[Display(Name = Display.Email)]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = Display.OldPassword)]
		[StringLength(DataConstants.Student.PasswordMaxLength, ErrorMessage = DataConstants.Error.PasswordLength, MinimumLength = DataConstants.Student.PasswordMinLength)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = Display.NewPassword)]
        [StringLength(DataConstants.Student.PasswordMaxLength, ErrorMessage = DataConstants.Error.PasswordLength, MinimumLength = DataConstants.Student.PasswordMinLength)]
        public string NewPassword { get; set; }
	}
}
