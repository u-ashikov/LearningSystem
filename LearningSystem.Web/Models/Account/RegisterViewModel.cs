namespace LearningSystem.Web.Models.Account
{
	using Common.Constants;
	using System;
	using System.ComponentModel.DataAnnotations;

    using static Common.Constants.WebConstants;

	public class RegisterViewModel
    {
		[Required]
		[StringLength(DataConstants.Student.UsernameMaxLength, MinimumLength = DataConstants.Student.UsernameMinLength, ErrorMessage = DataConstants.Error.NameLength)]
		public string Username { get; set; }

		[Required]
		[StringLength(DataConstants.Student.NameMaxLength, MinimumLength = DataConstants.Student.NameMinLength, ErrorMessage = DataConstants.Error.NameLength)]
		public string Name { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = Display.Email)]
        public string Email { get; set; }

        [Required]
        [StringLength(DataConstants.Student.PasswordMaxLength, ErrorMessage = DataConstants.Error.PasswordLength, MinimumLength = DataConstants.Student.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = Display.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = Display.ConfirmPassword)]
        [Compare("Password", ErrorMessage = DataConstants.Error.PasswordsMismatch)]
        public string ConfirmPassword { get; set; }
    }
}
