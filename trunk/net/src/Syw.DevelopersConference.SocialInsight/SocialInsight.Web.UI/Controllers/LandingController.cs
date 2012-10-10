using System;
using System.Web.Mvc;
using SocialInsight.Domain.Configuration;

namespace SocialInsight.Web.UI.Controllers
{
    public class LandingController : Controller
    {
	    private PlatformSettings _platformSettings;
	    private ApplicationSettings _applicationSettings;

	    public LandingController()
	    {
		    _platformSettings = new PlatformSettings();
		    _applicationSettings = new ApplicationSettings();
	    }

	    public ActionResult Index()
	    {
		    var model = new LandingModel
			    {
				    AppLoginUrl =
					    _platformSettings.SywWebSiteUrl + String.Format(_platformSettings.SywAppLoginUrl, _applicationSettings.AppId)
			    };

			return View("~/Views/Landing.cshtml", model);
        }
    }

	public class LandingModel
	{
		public string AppLoginUrl { get; set; }
	}
}
