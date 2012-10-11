using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SocialInsight.Domain.Dashboard;

namespace SocialInsight.Web.UI.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IDashboardFlow _dashboardFlow;

		public DashboardController()
		{
			_dashboardFlow = new DashboardFlow();
		}

		public ActionResult Index()
		{
			return View("~/Views/Dashboard.cshtml", new DashboardModel
				{
					Products = _dashboardFlow.GetScoredProducts().Select(ToModel).ToArray()
				});
		}

		private ScoredProductModel ToModel(ScoredProduct scoredProduct)
		{
			return new ScoredProductModel
				{
					Id = scoredProduct.Product.Id,
					Name = scoredProduct.Product.Name,
					ImageUrl = scoredProduct.Product.ImageUrl,
					ProductUrl = scoredProduct.Product.ProductUrl,
					Score = scoredProduct.Score
				};
		}
	}

	public class DashboardModel
	{
		public IList<ScoredProductModel> Products { get; set; }
	}

	public class ScoredProductModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public string ProductUrl { get; set; }
		public decimal Score { get; set; }
	}
}
