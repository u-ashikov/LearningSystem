namespace LearningSystem.Web.Controllers
{
    using Common.Constants;
    using Infrastructure.Enums;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Students;
    using Services.Contracts;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using static Common.Constants.WebConstants;

    [Authorize]
	[Route(Routing.StudentsBaseRoute)]
	public class StudentsController : BaseController
	{
		private readonly IStudentService students;

		private readonly ICourseService courses;

		private readonly UserManager<User> userManager;

		public StudentsController(IStudentService students, UserManager<User> userManager, ICourseService courses)
		{
			this.students = students;
			this.userManager = userManager;
			this.courses = courses;
		}

		[Route(Routing.SignUpForCourse)]
		public async Task<IActionResult> SignUpForCourse(int courseId)
		{
			var studentId = this.userManager.GetUserId(User);
			var student = await this.userManager.FindByIdAsync(studentId);

			if (!this.courses.CourseExist(courseId))
			{
				this.GenerateMessage(string.Format(NonExistingCourse, courseId), Alert.Warning);
				return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
			}

            if (this.courses.CourseStarted(courseId))
            {
                this.GenerateMessage(CourseStarted, Alert.Warning);
                return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
            }

            if (student == null)
			{
				this.GenerateMessage(string.Format(NonExistingUser, studentId), Alert.Warning);
				return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
			}

			if (this.students.IsInCourse(studentId, courseId))
			{
				this.GenerateMessage(AlreadyInCourse, Alert.Warning);
				return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
			}

			this.students.SignUpForCourse(studentId, courseId);
			var courseName = this.courses.GetCourseName(courseId);

			this.GenerateMessage(string.Format(SuccessSignUpForCourse, courseName), Alert.Success);

			return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
		}

		[Route(Routing.SignOutFromCourse)]
		public async Task<IActionResult> SignOutFromCourse(int courseId)
		{
			var studentId = this.userManager.GetUserId(User);
			var student = await this.userManager.FindByIdAsync(studentId);

			if (!this.courses.CourseExist(courseId))
			{
				this.GenerateMessage(string.Format(NonExistingCourse, courseId), Alert.Warning);
				return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
			}

            if (this.courses.CourseStarted(courseId))
            {
                this.GenerateMessage(CourseStarted, Alert.Warning);
                return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
            }

            if (student == null)
			{
				this.GenerateMessage(string.Format(NonExistingUser, studentId), Alert.Warning);
				return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
			}

            if (!this.students.IsInCourse(studentId, courseId))
			{
				this.GenerateMessage(string.Format(NotInCourse,studentId), Alert.Warning);
				return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
			}

			this.students.SignOutFromCourse(studentId, courseId);

			var courseName = this.courses.GetCourseName(courseId);

			this.GenerateMessage(string.Format(SuccessSignOutFromCourse, courseName), Alert.Success);

			return RedirectToAction(nameof(CoursesController.All), WebConstants.Controller.Courses);
		}

		[Route(Routing.MyCourses)]
		public IActionResult MyCourses(string id)
		{
			if (this.userManager.GetUserId(User) != id)
			{
				this.GenerateMessage(NotProfileOwner, Alert.Danger);
				return RedirectToAction(nameof(HomeController.Index), WebConstants.Controller.Home);
			}

			return View(this.students.GetStudentCourses(id));
		}

		[Route(Routing.UploadSolution)]
		public IActionResult UploadSolution(int courseId)
		{
			var studentId = this.userManager.GetUserId(User);

			if (!this.courses.CourseExist(courseId))
			{
				this.GenerateMessage(string.Format(NonExistingCourse, courseId), Alert.Warning);
				return RedirectToAction(nameof(MyCourses), new { id = studentId });
			}

			if (!this.students.IsInCourse(studentId,courseId))
			{
                this.GenerateMessage(string.Format(NotInCourse, studentId), Alert.Warning);
                return RedirectToAction(nameof(MyCourses), new { id = studentId });
			}

            if (!this.courses.IsCourseLastDay(courseId))
            {
                this.GenerateMessage(NotCourseLastDay, Alert.Warning);
                return RedirectToAction(nameof(MyCourses), new { id = studentId });
            }

			var course = this.courses.GetCourseInfo(courseId);

			return View(new UploadCourseSolutionFormModel()
			{
				CourseId = courseId,
				Name = course.Name,
				StartDate = course.StartDate,
				EndDate = course.EndDate
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route(Routing.UploadSolution)]
		public async Task<IActionResult> UploadSolution(int courseId, UploadCourseSolutionFormModel uploadExamForm)
		{
            if (!ModelState.IsValid)
            {
                return View(uploadExamForm);
            }

            var studentId = this.userManager.GetUserId(User);

			if (!this.courses.CourseExist(courseId) 
                || !this.students.IsInCourse(studentId, courseId)
                || !this.courses.IsCourseLastDay(courseId))
			{
				return BadRequest();
			}

			await this.students.UploadSolution(courseId, studentId, uploadExamForm.File);

			this.GenerateMessage(string.Format(SuccessUploadSolutionForCourse, uploadExamForm.Name), Alert.Success);

			return RedirectToAction(nameof(MyCourses), new { id = studentId });
		}

		[Route(Routing.CourseCertificate)]
		public IActionResult CourseCertificate(int courseId)
		{
            var userId = this.userManager.GetUserId(User);

			var certificateInfo = this.students.GetCourseCertificate(courseId, userId);

			if (certificateInfo == null)
			{
				return BadRequest();
			}

			using (var ms = new MemoryStream())
			{
				Document doc = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
				PdfWriter writer = PdfWriter.GetInstance(doc, ms);
				doc.Open();

				doc.Add(new Paragraph("COURSE CERTIFICATE") { Alignment = Element.ALIGN_CENTER, Font = new Font(Font.TIMES_ROMAN, 18, Font.BOLD) });
				doc.Add(Chunk.Newline);

				doc.Add(new Paragraph("This certificate is issued by Learning System to acknowledge that") { Alignment = Element.ALIGN_CENTER, Font = new Font(Font.TIMES_ROMAN, 18, Font.BOLD) });
				doc.Add(Chunk.Newline);

				doc.Add(new Paragraph(certificateInfo.StudentName) { Alignment = Element.ALIGN_CENTER, Font = new Font(Font.TIMES_ROMAN, 18, Font.BOLD) });
				doc.Add(Chunk.Newline);

				doc.Add(new Paragraph("has successfully completed a course") { Alignment = Element.ALIGN_CENTER, Font = new Font(Font.TIMES_ROMAN, 18, Font.BOLD) });
				doc.Add(Chunk.Newline);

				doc.Add(new Paragraph($"{certificateInfo.CourseName} {certificateInfo.StartDate} - {certificateInfo.EndDate}") { Alignment = Element.ALIGN_CENTER, Font = new Font(Font.TIMES_ROMAN, 18, Font.BOLD) });
 				doc.Add(Chunk.Newline);

				doc.Add(new Paragraph($"Trainer: {certificateInfo.Trainer}") { Alignment = Element.ALIGN_CENTER, Font = new Font(Font.TIMES_ROMAN, 18, Font.BOLD) });
				doc.Add(Chunk.Newline);

				doc.Add(new Paragraph($"with Grade: {certificateInfo.Grade.ToString()}") { Alignment = Element.ALIGN_CENTER, Font = new Font(Font.TIMES_ROMAN, 18, Font.BOLD) });
				doc.Add(Chunk.Newline);

				doc.Add(new Paragraph($"Issue date: {DateTime.Now.ToShortDateString()}") { Alignment = Element.ALIGN_LEFT, Font = new Font(Font.TIMES_ROMAN, 18, Font.BOLD) });
				doc.Add(Chunk.Newline);

				doc.Close();

				byte[] bytes = ms.ToArray();

				return File(bytes, CertificateContentType, string.Format(CertificateDownloadName, certificateInfo.CourseName));
			}
		}
	}
}
