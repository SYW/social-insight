using System;
using System.Web.Mvc;
using Platform.Client.Common.Context;
using SocialInsight.Domain;
using SocialInsight.Domain.AppLinks;
using SocialInsight.Domain.Catalogs;
using SocialInsight.Domain.Configuration;
using SocialInsight.Domain.Users;

namespace SocialInsight.Web.UI.Controllers
{
    public class PostLoginController : Controller
    {
	    private readonly Routes _routes;
	    private readonly IUsersApi _usersApi;
		private readonly ICreateCatalogFlow _createCatalogFlow;
	    private readonly ApplicationSettings _applicationSettings;
	    private readonly IAppLinksApi _appLinksApi;

	    public PostLoginController()
	    {
		    _routes = new Routes();
		    var contextProvider = new HttpContextProvider();
		    _usersApi = new UsersApi(contextProvider);
		    _createCatalogFlow = new CreateCatalogFlow(contextProvider, new CookieStateProvider());
		    _applicationSettings = new ApplicationSettings();
		    _appLinksApi = new AppLinksApi();
	    }

	    [RequireHttps]
        public ActionResult Index()
	    {
			_createCatalogFlow.Create();
			_appLinksApi.CreateAppLink(_applicationSettings.AppLinkTitle, _applicationSettings.AppLink);

		    var model = new PostLoginModel
			    {
				    DashboardUrl = _routes.Dashboard(),
					UserName = _usersApi.GetCurrentUser().Name
			    };

            return View("~/Views/PostLogin.cshtml", model);
        }
    }

	public class PostLoginModel
	{
		public string UserName { get; set; }
		public Uri DashboardUrl { get; set; }
	}
}
