namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Web.Controllers;

    using static Common.Constants.WebConstants;

    [Area(Area.Blog)]
	[Route(Routing.BlogBaseRoute)]
	[Authorize(Roles = BlogAuthorRole)]
	public abstract class BlogController : BaseController
    {
    }
}
