using System.Web.Mvc;
using SocialInsight.Domain;
using SocialInsight.Domain.Users;

namespace SocialInsight.Web.UI.Controllers
{
    public class PostLoginController : Controller
    {
	    private readonly Routes _routes;
	    private IUsersApi _usersApi; 

	    public PostLoginController()
	    {
		    _routes = new Routes();
			_usersApi = new UsersApi();
	    }

	    [RequireHttps]
        public ActionResult Index()
	    {
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
		public string DashboardUrl { get; set; }
	}
}
