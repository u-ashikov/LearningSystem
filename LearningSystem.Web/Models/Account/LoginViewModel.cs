namespace LearningSystem.Web.Models.Account
{
	using System.ComponentModel.DataAnnotations;

    using static Common.Constants.WebConstants;

	public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = Display.RememberMe)]
        public bool RememberMe { get; set; }
    }
}
