namespace LearningSystem.Web.Models.Manage
{
	using System.ComponentModel.DataAnnotations;

    using static Common.Constants.WebConstants;

	public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = Display.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
