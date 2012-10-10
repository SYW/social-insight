using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialInsight.Web.UI.Controllers
{
    public class LoginController : Controller
    {
		[RequireHttps]
        public ActionResult Index()
        {
            return View("~/Views/Login.cshtml");
        }
    }
}
