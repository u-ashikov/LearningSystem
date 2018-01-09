namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Common.Constants;
    using LearningSystem.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Common.Constants.WebConstants;

    [Area(Area.Admin)]
	[Route(Routing.AdminBaseRoute)]
	[Authorize(Roles = AdminConstants.Role)]
	public abstract class AdminController : BaseController
    {
    }
}
