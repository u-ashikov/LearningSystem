namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Home;
    using Services.Contracts;
    using System.Diagnostics;

    public class HomeController : Controller
    {
		private readonly IUserService users;

		private readonly ICourseService courses;

		public HomeController(IUserService users, ICourseService courses)
		{
			this.users = users;
			this.courses = courses;
		}

        public IActionResult Index()
        {
            return View(new SearchFormModel());
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult SearchResults(SearchFormModel model)
		{
			if (string.IsNullOrEmpty(model.SearchTerm) || (!model.InCourses && !model.InUsers))
			{
				return RedirectToAction(nameof(Index));
			}

            var searchResults = new SearchResultsViewModel()
            {
                SearchTerm = model.SearchTerm
            };

			if (model.InCourses)
			{
				searchResults.Courses = this.courses.SearchCourses(model.SearchTerm);
			}

			if (model.InUsers)
			{
				searchResults.Users = this.users.SearchUsers(model.SearchTerm);
			}

			return View(searchResults);
		}

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
