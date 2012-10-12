using System.Web.Mvc;
using Platform.Client.Common.Context;
using SocialInsight.Domain;
using SocialInsight.Domain.Auth;
using SocialInsight.Domain.Users;

namespace SocialInsight.Web.UI.Controllers
{
    public class LandingController : Controller
    {
	    private readonly Routes _routes;
	    private readonly IAuthApi _authApi;
		private readonly IUsersApi _usersApi;

	    public LandingController()
	    {
		    _routes = new Routes();
		    var contextProvider = new HttpContextProvider();
		    _authApi = new AuthApi(contextProvider);
		    _usersApi = new UsersApi(contextProvider);
	    }

	    public ActionResult Index()
	    {
		    var userState = _authApi.GetUserState();

			var model = new LandingModel
			    {
				    AppLoginUrl = _routes.Login(),
					IsUserLoggedIn = userState != UserState.Anonymous,
					UserAlreadyInstalledApp = userState == UserState.Authorized,					
			    };

			if (model.IsUserLoggedIn)
				model.UserName = _usersApi.GetCurrentUser().Name;

			if (model.UserAlreadyInstalledApp)
				model.DashboardUrl = _routes.Dashboard();

		    return View("~/Views/Landing.cshtml", model);
        }
    }

	public class LandingModel
	{
		public string AppLoginUrl { get; set; }
		public bool IsUserLoggedIn { get; set; }
		public bool UserAlreadyInstalledApp { get; set; }
		public string DashboardUrl { get; set; }
		public string UserName { get; set; }
	}
}
