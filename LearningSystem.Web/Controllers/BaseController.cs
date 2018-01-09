namespace LearningSystem.Web.Controllers
{
	using Infrastructure.Enums;
	using Microsoft.AspNetCore.Mvc;

    using static Common.Constants.WebConstants.TempDataKey;

	public abstract class BaseController : Controller
    {
		protected void GenerateMessage(string message, Alert type)
		{
			this.TempData[Message] = message;
			this.TempData[AlertType] = type.ToString().ToLower();
		}
    }
}
