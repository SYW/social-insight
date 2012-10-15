using System;
using System.Web.Mvc;
using Platform.Client.Common.Context;
using SocialInsight.Domain;
using SocialInsight.Domain.AppActions;
using SocialInsight.Domain.Auth;
using SocialInsight.Domain.Users;

namespace SocialInsight.Web.UI.Controllers
{
    public class LandingController : Controller
    {
	    private readonly Routes _routes;
	    private readonly IAuthApi _authApi;
		private readonly IUsersApi _usersApi;
	    private readonly IAppActionsApi _appActionsApi;

	    public LandingController()
	    {
		    _routes = new Routes();
		    var contextProvider = new HttpContextProvider();
		    _authApi = new AuthApi(contextProvider);
		    _usersApi = new UsersApi(contextProvider);
		    _appActionsApi = new AppActionsApi(contextProvider);
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

			// This should be done only once - by a utility or service (using offline token of course). No need to do it for every user.
			// For the purpose of the demonstration, this will be fine here (remember - big NO NO in production apps)
			_appActionsApi.Register("Add to Social Insight", 300, 300);

		    return View("~/Views/Landing.cshtml", model);
        }
    }

	public class LandingModel
	{
		public string AppLoginUrl { get; set; }
		public bool IsUserLoggedIn { get; set; }
		public bool UserAlreadyInstalledApp { get; set; }
		public Uri DashboardUrl { get; set; }
		public string UserName { get; set; }
	}
}
