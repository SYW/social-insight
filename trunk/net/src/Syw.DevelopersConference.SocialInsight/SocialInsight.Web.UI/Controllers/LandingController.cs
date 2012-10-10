using System;
using System.Web.Mvc;
using SocialInsight.Domain;
using SocialInsight.Domain.Configuration;

namespace SocialInsight.Web.UI.Controllers
{
    public class LandingController : Controller
    {
	    private readonly Routes _routes;

	    public LandingController()
	    {
		    _routes = new Routes();
	    }

	    public ActionResult Index()
	    {
		    var model = new LandingModel
			    {
				    AppLoginUrl = _routes.Login()
			    };

			return View("~/Views/Landing.cshtml", model);
        }
    }

	public class LandingModel
	{
		public string AppLoginUrl { get; set; }
	}
}
