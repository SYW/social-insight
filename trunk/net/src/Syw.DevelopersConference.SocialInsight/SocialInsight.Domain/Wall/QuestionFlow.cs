using System;
using Platform.Client.Common.Context;
using SocialInsight.Domain.Products;
using System.Linq;

namespace SocialInsight.Domain.Wall
{
	public class QuestionFlow
	{
		private readonly IWallApi _wallApi;
		private readonly IProductsApi _productsApi;

		public QuestionFlow(IContextProvider contextProvider)
		{
			_wallApi = new WallApi(contextProvider);
			_productsApi = new ProductsApi(contextProvider);
		}

		public void Ask(string question, long productId)
		{
			if (string.IsNullOrEmpty(question))
				throw new ArgumentNullException("question", "Can not ask without a question");

			var product = _productsApi.Get(new[] {productId}).FirstOrDefault();
			if (product == null)
				throw new ArgumentException(string.Format("Product {0} does not exist", productId), "productId");

			_wallApi.PublishUserAction("wants more information about " + product.Name, string.Empty, question, product.ImageUrl, product.ProductUrl);
		}
	}
}
