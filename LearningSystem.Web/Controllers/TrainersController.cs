namespace LearningSystem.Web.Controllers
{
    using Common.Constants;
    using Infrastructure.Enums;
    using LearningSystem.Data.Enums;
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Trainers;
    using Services.Contracts;
    using System.Threading.Tasks;

    using static Common.Constants.WebConstants;

    [Authorize(Roles = TrainerRole)]
    public class TrainersController : BaseController
    {
        private readonly ITrainerService trainers;

        private readonly ICourseService courses;

        private readonly IStudentService students;

        private readonly UserManager<User> userManager;

        public TrainersController(ITrainerService trainers, UserManager<User> userManager, ICourseService courses, IStudentService students)
        {
            this.trainers = trainers;
            this.courses = courses;
            this.students = students;
            this.userManager = userManager;
        }

        [Route(Routing.TrainerCourses)]
        public IActionResult TrainedCourses(string id, int page = 1)
        {
            if (page < MinPageSize)
            {
                return RedirectToAction(nameof(TrainedCourses), new { id, page = MinPageSize });
            }

            var loggedInUserId = this.userManager.GetUserId(User);

            if (loggedInUserId != id)
            {
                this.GenerateMessage(NotProfileOwner, Alert.Warning);
                return RedirectToAction(nameof(AccountController.Profile), WebConstants.Controller.Account, new { id = loggedInUserId });
            }

            var pagination = new PaginationViewModel()
            {
                Action = nameof(TrainedCourses),
                PageSize = TrainerCoursesPageSize,
                CurrentPage = page,
                TotalElements = this.trainers.TrainerTotalCourses(id)
            };

            if (page > pagination.TotalPages && pagination.TotalPages != 0)
            {
                return RedirectToAction(nameof(TrainedCourses), new { id, page = pagination.TotalPages });
            }

            return View(new TrainerCoursesViewModel()
            {
                Courses = this.trainers.TrainedCourses(id, page, TrainerCoursesPageSize),
                Pagination = pagination
            });
        }

        public IActionResult CourseStudents(int id)
        {
            if (!this.courses.CourseExist(id))
            {
                this.GenerateMessage(string.Format(NonExistingCourse, id), Alert.Warning);
                return RedirectToAction(nameof(HomeController.Index), WebConstants.Controller.Home);
            }

            var loggedInUserId = this.userManager.GetUserId(User);

            if (!this.trainers.IsCourseTrainer(id, loggedInUserId))
            {
                this.GenerateMessage(NotCourseTrainer, Alert.Warning);
                return RedirectToAction(nameof(AccountController.Profile), WebConstants.Controller.Account, new { id = loggedInUserId });
            }

            if (!this.courses.CourseFinished(id))
            {
                this.GenerateMessage(CourseInProgress, Alert.Warning);
                return RedirectToAction(nameof(AccountController.Profile), WebConstants.Controller.Account, new { id = loggedInUserId });
            }

            return View(this.trainers.GetCourseStudentsForAssesment(id, loggedInUserId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssesStudent(int courseId, string studentId, Grade grade)
        {
            var trainerId = this.userManager.GetUserId(User);

            if (!this.courses.CourseExist(courseId)
                || await this.userManager.FindByIdAsync(studentId) == null
                || !this.trainers.IsCourseTrainer(courseId, trainerId)
                || !this.courses.CourseFinished(courseId))
			{
				return BadRequest();
			}

			if (!this.students.IsInCourse(studentId,courseId))
			{
				this.GenerateMessage(string.Format(NotInCourse, studentId), Alert.Warning);

				return RedirectToAction(nameof(CourseStudents), new { id = courseId });
			}

			this.trainers.AssesStudent(courseId, studentId, grade);

			return RedirectToAction(nameof(CourseStudents), new { id = courseId });
		}

		[Route(Routing.DownloadSolutionById)]
		public async Task<IActionResult> DownloadSolution(int id, string studentId)
		{
			var trainerId = this.userManager.GetUserId(User);

			if (await this.userManager.FindByIdAsync(studentId) == null)
			{
				this.GenerateMessage(string.Format(NonExistingStudent, studentId), Alert.Warning);
				return RedirectToAction(nameof(CourseStudents), new { id });
			}

			if (!this.trainers.IsCourseTrainer(id, trainerId))
			{
				this.GenerateMessage(NotCourseTrainer, Alert.Warning);
				return RedirectToAction(nameof(CourseStudents), new { id });
			}

			if (!this.students.IsInCourse(studentId,id))
			{
				this.GenerateMessage(string.Format(NotInCourse,studentId), Alert.Warning);
				return RedirectToAction(nameof(CourseStudents), new { id });
			}

            if (!this.courses.CourseFinished(id))
            {
                this.GenerateMessage(CourseInProgress, Alert.Warning);
                return RedirectToAction(nameof(CourseStudents), new { id });
            }

			var file = this.trainers.GetStudentSolution(id, studentId);

			return File(file, SolutionContentType,string.Format(SolutionDownloadName,studentId));
		}
    }
}
