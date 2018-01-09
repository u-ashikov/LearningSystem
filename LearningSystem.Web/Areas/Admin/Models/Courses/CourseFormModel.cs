namespace LearningSystem.Web.Areas.Admin.Models.Courses
{
    using Common.Constants;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.Constants.WebConstants;

    public class CourseFormModel
    {
		[Required]
		[StringLength(DataConstants.Course.NameMaxLength, MinimumLength = DataConstants.Course.NameMinLength, ErrorMessage = DataConstants.Error.NameLength)]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Display(Name = Display.Trainer)]
		public string TrainerId { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = Display.StartDate)]
		public DateTime StartDate { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = Display.EndDate)]
		public DateTime EndDate { get; set; }

		public IEnumerable<SelectListItem> Trainers { get; set; }
	}
}
