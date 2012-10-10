using System.Web.Mvc;
using SocialInsight.Domain.Context;

namespace SocialInsight.Web.UI.Filters
{
	public class TokenExtractingFilter : ActionFilterAttribute
	{
		private readonly PlatformToken _platformToken;

		public TokenExtractingFilter()
		{
			_platformToken = new PlatformToken(new HttpContextProvider());
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			var token = filterContext.HttpContext.Request.QueryString["token"];

			if (string.IsNullOrEmpty(token))
				return;

			_platformToken.Set(token);
		}
	}
}