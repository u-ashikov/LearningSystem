namespace LearningSystem.Web.Controllers
{
    using Common.Constants;
    using Infrastructure.Enums;
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Courses;
    using Services.Contracts;

    using static Common.Constants.WebConstants;

    [Route(Routing.CoursesBaseRoute)]
	public class CoursesController : BaseController
    {
		private readonly ICourseService courses;

		private readonly UserManager<User> userManager;

		public CoursesController(ICourseService courses, UserManager<User> userManager)
		{
			this.courses = courses;
			this.userManager = userManager;
		}

		[Route(Routing.AllCourses)]
		public IActionResult All(int page = 1)
        {
            if (page < MinPageSize)
            {
                return RedirectToAction(nameof(All), new { page = MinPageSize });
            }

            var pagination = new PaginationViewModel()
            {
                Action = nameof(All),
                PageSize = CoursesPageSize,
                CurrentPage = page,
                TotalElements = this.courses.TotalCourses()
            };

            if (page > pagination.TotalPages && pagination.TotalPages != 0)
            {
                return RedirectToAction(nameof(All), new { page = pagination.TotalPages });
            }

            return View(new AllCoursesViewModel()
            {
                Courses = this.courses.All(page, CoursesPageSize),
                Pagination = pagination
            });
        }

		[Route(Routing.CourseDetails)]
		public IActionResult Details(int id)
		{
			if (!this.courses.CourseExist(id))
			{
				this.GenerateMessage(string.Format(NonExistingCourse, id), Alert.Warning);
				return RedirectToAction(nameof(All));
			}

			return View(new CourseDetailsViewModel()
			{
				Course = this.courses.GetCourseDetailsById(id),
				IsUserInCourse = this.courses.IsStudentInCourse(id, this.userManager.GetUserId(User))
			});
		}
	}
}
