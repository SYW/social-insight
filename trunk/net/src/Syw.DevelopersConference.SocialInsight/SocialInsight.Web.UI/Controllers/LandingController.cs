using System.Web.Mvc;
using SocialInsight.Domain;
using SocialInsight.Domain.Auth;
using SocialInsight.Domain.Users;

namespace SocialInsight.Web.UI.Controllers
{
    public class LandingController : Controller
    {
	    private readonly Routes _routes;
	    private readonly AuthApi _authApi;
		private readonly IUsersApi _usersApi;

	    public LandingController()
	    {
		    _routes = new Routes();
		    _authApi = new AuthApi();
		    _usersApi = new UsersApi();
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
