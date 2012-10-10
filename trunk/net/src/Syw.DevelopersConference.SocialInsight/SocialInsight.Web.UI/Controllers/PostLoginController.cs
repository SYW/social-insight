using System.Web.Mvc;
using SocialInsight.Domain;

namespace SocialInsight.Web.UI.Controllers
{
    public class PostLoginController : Controller
    {
	    private readonly Routes _routes;

	    public PostLoginController()
	    {
		    _routes = new Routes();
	    }

	    [RequireHttps]
        public ActionResult Index()
	    {
		    var model = new PostLoginModel
			    {
				    DashboardUrl = _routes.Dashboard()
			    };

            return View("~/Views/PostLogin.cshtml", model);
        }

    }

	public class PostLoginModel
	{
		public string DashboardUrl { get; set; }
	}
}
