using System.Collections.Generic;
using System.Linq;
using Platform.Client.Common.Context;
using SocialInsight.Domain.Catalogs;
using SocialInsight.Domain.Context;
using SocialInsight.Domain.Products;

namespace SocialInsight.Domain.Dashboard
{
	public interface IDashboardFlow
	{
		IList<ScoredProduct> GetScoredProducts();
	}

	public class DashboardFlow : IDashboardFlow
	{
		private readonly IUserContextProvider _userContextProvider;
		private readonly IProductsApi _productsApi;
		private readonly IProductsRepository _productsRepository;
		private readonly IUserCatalogProvider _userCatalogProvider;

		public DashboardFlow(IContextProvider contextProvider,
			IStateProvider stateProvider)
		{
			_userContextProvider = new UserContextProvider(stateProvider, contextProvider);
			_productsApi = new ProductsApi(contextProvider);
			_productsRepository = new ProductsRepository();
			_userCatalogProvider = new UserCatalogProvider(contextProvider, stateProvider);
		}

		public IList<ScoredProduct> GetScoredProducts()
		{
			var userId = _userContextProvider.GetUserId();
			var userCatalog = _userCatalogProvider.Get(userId);
			if (userCatalog == null)
				return new ScoredProduct[0];

			var scoredProducts = _productsRepository.GetProductInsightForUser(userId).ToDictionary(x => x.ProductId);
			var products = _productsApi
				.Get(userCatalog.Items.Select(x => x.Id).ToArray());

			return products.Select(x => new ScoredProduct
				{
					Product = x,
					Score = scoredProducts.ContainsKey(x.Id) ? scoredProducts[x.Id].Score : 0
				}).ToArray();
		}
	}

	public class ScoredProduct
	{
		public ProductDto Product { get; set; }
		public decimal Score { get; set; }
	}
}
