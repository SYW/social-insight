using System;
using System.Web.Mvc;
using Platform.Client.Common.Context;
using SocialInsight.Domain;
using SocialInsight.Domain.Products;

namespace SocialInsight.Web.UI.Controllers
{
	public class ProductController : Controller
	{
		private readonly CatalogItemsFlow _catalogItemsFlow;
		private Routes _routes;

		public ProductController()
		{
			_catalogItemsFlow = new CatalogItemsFlow(new HttpContextProvider(), new CookieStateProvider());
			_routes = new Routes();
		}

		public ActionResult Index(long productId)
		{
			var model = new ProductActionModel
				{
					HasProductBeenAdded = _catalogItemsFlow.AddProduct(productId),
					DashboardUrl = _routes.Dashboard()
				};

			return View("~/Views/ProductAddedSuccessfully.cshtml", model);
		}
	}

	public class ProductActionModel
	{
		public bool HasProductBeenAdded { get; set; }
		public Uri DashboardUrl { get; set; }
	}
}