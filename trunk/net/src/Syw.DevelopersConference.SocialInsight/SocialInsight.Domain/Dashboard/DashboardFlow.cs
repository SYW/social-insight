using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SocialInsight.Domain.Catalogs;
using SocialInsight.Domain.Context;
using SocialInsight.Domain.Products;
using SocialInsight.Domain.Users;

namespace SocialInsight.Domain.Dashboard
{
	public interface IDashboardFlow
	{
		IList<ScoredProduct> GetScoredProducts();
	}

	public class DashboardFlow : IDashboardFlow
	{
		private readonly IUsersApi _usersApi;
		private readonly IUserContextProvider _userContextProvider;
		private readonly ICatalogsApi _catalogsApi;
		private readonly ICatalogsRepository _catalogsRepository;
		private readonly ICreateCatalogFlow _createCatalogFlow;
		private readonly IProductsApi _productsApi;

		public DashboardFlow()
		{
			_usersApi = new UsersApi();
			_userContextProvider = new UserContextProvider();
			_catalogsApi = new CatalogsApi();
			_catalogsRepository = new CatalogsRepository();
			_createCatalogFlow = new CreateCatalogFlow();
			_productsApi = new ProductsApi();
		}

		public IList<ScoredProduct> GetScoredProducts()
		{
			var userId = _userContextProvider.GetUserId();
			var scoredProducts = GetProductsToScore(userId).Select(x => new ScoredProduct { Product = x, Score = 0 }).ToArray();
			if (!scoredProducts.Any())
				return new ScoredProduct[0];

			var productsCount = CountFriendsProducts(userId);
			if (!productsCount.Any())
				return scoredProducts;

			foreach (var product in scoredProducts.Where(product => productsCount.ContainsKey(product.Product.Id)))
			{
				product.Score = productsCount[product.Product.Id];
			}

			return scoredProducts;
		}

		private IDictionary<long, int> CountFriendsProducts(long userId)
		{
			var friends = _usersApi.GetFriends(userId);
			if (!friends.Any())
				return new Dictionary<long, int>();

			var usersCatalogs = _catalogsApi.GetUsersCatalogs(friends);
			var items = usersCatalogs.SelectMany(x => x.Items).Select(x => x.Id);

			return items
				.GroupBy(x => x, (x, y) => new { Id = x, Count = y.Count() })
				.ToDictionary(x => x.Id, y => y.Count);			
		}

		private IList<ProductDto> GetProductsToScore(long userId)
		{
			var userCatalogId = _catalogsRepository.GetUserCatalog(userId);
			if (!userCatalogId.HasValue)
				throw new InvalidOperationException("Social Insight catalog was not created for user #" + userId);

			var userCatalog = _catalogsApi.Get(new[] { userCatalogId.Value }).FirstOrDefault();
			if (userCatalog == null)
			{
				_createCatalogFlow.Create();
				return new ProductDto[0];
			}

			if (!userCatalog.Items.Any())
				return new ProductDto[0];

			return _productsApi.Get(userCatalog.Items.Select(x => x.Id).ToArray());			
		}
	}

	public class ScoredProduct
	{
		public ProductDto Product { get; set; }
		public decimal Score { get; set; }
	}
}
