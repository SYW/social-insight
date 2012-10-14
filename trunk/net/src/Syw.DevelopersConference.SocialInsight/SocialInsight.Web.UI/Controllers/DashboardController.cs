using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Platform.Client.Common.Context;
using Platform.Client.Configuration;
using SocialInsight.Domain.Configuration;
using SocialInsight.Domain.Dashboard;

namespace SocialInsight.Web.UI.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IDashboardFlow _dashboardFlow;
		private readonly IPlatformSettings _platformSettings;

		public DashboardController()
		{
			_dashboardFlow = new DashboardFlow(new HttpContextProvider(), 
				new CookieStateProvider());

			_platformSettings = new PlatformSettings();
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
					ProductUrl = GetProductUrl(scoredProduct),
					Score = scoredProduct.Score
				};
		}

		private Uri GetProductUrl(ScoredProduct scoredProduct)
		{
			return string.IsNullOrEmpty(scoredProduct.Product.ProductUrl) ? null : new Uri(_platformSettings.SywWebSiteUrl, scoredProduct.Product.ProductUrl);
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
		public Uri ProductUrl { get; set; }
		public decimal Score { get; set; }
	}
}
