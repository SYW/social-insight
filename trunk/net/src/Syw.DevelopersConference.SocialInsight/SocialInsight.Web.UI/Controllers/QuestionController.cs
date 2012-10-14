using System.Web.Mvc;
using Platform.Client.Common.Context;
using SocialInsight.Domain.Wall;

namespace SocialInsight.Web.UI.Controllers
{
    public class QuestionController : Controller
    {
	    private readonly QuestionFlow _questionFlow;

	    public QuestionController()
	    {
		    _questionFlow = new QuestionFlow(new HttpContextProvider());
	    }

	    public ActionResult Ask(string question, long productId)
        {
			_questionFlow.Ask(question, productId);

            return Content("OK");
        }

    }
}
