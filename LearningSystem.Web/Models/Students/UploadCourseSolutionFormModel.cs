namespace LearningSystem.Web.Models.Students
{
    using Common.Constants;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UploadCourseSolutionFormModel : IValidatableObject
    {
		public int CourseId { get; set; }

		public string Name { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public IFormFile File { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (this.File.Length > DataConstants.Solution.SolutionMaxFileSize)
			{
				yield return new ValidationResult(WebConstants.FileSizeError);
			}

			if (!this.File.FileName.EndsWith(WebConstants.FileAvailableExtension))
			{
				yield return new ValidationResult(WebConstants.FileAcceptedFormatError);
			}
		}
	}
}
