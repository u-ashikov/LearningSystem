namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Common.Constants;
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Courses;
    using Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Web.Controllers;
    using Web.Infrastructure.Enums;

    using static Common.Constants.WebConstants;

    public class AdminCoursesController : AdminController
    {
        private const int StartDateDaysIncrementor = 1;

        private const int EndDateDaysIncremetor = 30;

		private readonly IAdminCourseService courses;

		private readonly IUserService users;

		public AdminCoursesController(IAdminCourseService courses, IUserService users, UserManager<User> userManager)
		{
			this.courses = courses;
			this.users = users;
		}

		[Route(Routing.AdminAddCourse)]
		public async Task<IActionResult> Add() => 
			View(new CourseFormModel()
			{
				StartDate = DateTime.Now.AddDays(StartDateDaysIncrementor),
				EndDate = DateTime.Now.AddDays(EndDateDaysIncremetor),
				Trainers = await GenerateTrainersSelectListItems()
			});

		[HttpPost]
		[Route(Routing.AdminAddCourse)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(CourseFormModel course)
		{
			if (!ModelState.IsValid)
			{
				course.Trainers = await GenerateTrainersSelectListItems();
				return View(course);
			}

			if (course.StartDate <= DateTime.Now)
			{
				return await InvalidCourseDates(course, CourseStartDateInFuture);
			}

			if (course.EndDate <= course.StartDate)
			{
				return await InvalidCourseDates(course, CourseEndDateAfterStartDate);
			}

			var trainerName = this.users.GetNameById(course.TrainerId);

			this.courses.Add(course.Name, course.Description, course.TrainerId, course.StartDate, course.EndDate);

			this.GenerateMessage(string.Format(SuccessCourseCreation, course.Name,trainerName), Alert.Success);

			return RedirectToAction(nameof(CoursesController.All),WebConstants.Controller.Courses);
		}

		[Route(Routing.AdminEditCourse)]
		public async Task<IActionResult> Edit(int id)
		{
			if (!this.courses.CourseExist(id))
			{
				this.GenerateMessage(string.Format(NonExistingCourse, id), Alert.Warning);
				return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
			}

			var course = this.courses.GetCourseToEdit(id);

			return View(new CourseFormModel()
			{
				Name = course.Name,
				Description = course.Description,
				StartDate = course.StartDate,
				EndDate = course.EndDate,
				TrainerId = course.TrainerId,
				Trainers = await this.GenerateTrainersSelectListItems()
			});
		}

		[HttpPost]
		[Route(Routing.AdminEditCourse)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id,CourseFormModel course)
		{
            if (!ModelState.IsValid)
            {
                course.Trainers = await this.GenerateTrainersSelectListItems();
                return View(course);
            }

            if (!this.courses.CourseExist(id))
			{
				this.GenerateMessage(string.Format(NonExistingCourse, id), Alert.Warning);
				return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
			}

			if (course.StartDate <= DateTime.Now)
			{
				return await InvalidCourseDates(course, CourseStartDateInFuture);
			}

			if (course.EndDate <= course.StartDate)
			{
				return await InvalidCourseDates(course, CourseEndDateAfterStartDate);
			}

			this.courses.Edit(id, course.Name, course.Description, course.TrainerId,course.StartDate, course.EndDate);

			this.GenerateMessage(string.Format(SuccessCourseEdit, id), Alert.Success);

			return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
		}

		private async Task<IList<SelectListItem>> GenerateTrainersSelectListItems()
		{
			var trainers = await this.users.GetTrainers();

			var trainersSelectItems = new List<SelectListItem>();

			foreach (var trainer in trainers)
			{
				trainersSelectItems.Add(new SelectListItem()
				{
					Text = trainer.Name,
					Value = trainer.Id
				});
			}

			return trainersSelectItems;
		}

		private async Task<IActionResult> InvalidCourseDates(CourseFormModel course, string message)
		{
			course.Trainers = await GenerateTrainersSelectListItems();
			ModelState.AddModelError(string.Empty, message);
			return View(course);
		}
	}
}
