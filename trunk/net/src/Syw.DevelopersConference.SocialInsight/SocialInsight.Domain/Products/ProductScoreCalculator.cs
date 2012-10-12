using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Client.Common.Context;
using SocialInsight.Domain.Catalogs;
using SocialInsight.Domain.Context;
using SocialInsight.Domain.Users;

namespace SocialInsight.Domain.Products
{
	public interface IProductScoreCalculator
	{
		IList<ProductScore> CalculateForUser(long userId);
	}

	public class ProductScoreCalculator : IProductScoreCalculator
	{
		private readonly IUsersApi _usersApi;
		private readonly ICatalogsApi _catalogsApi;
		private readonly IUserCatalogProvider _userCatalogProvider;

		public ProductScoreCalculator(IContextProvider contextProvider,
			IStateProvider stateProvider)
		{
			_usersApi = new UsersApi(contextProvider);
			_catalogsApi = new CatalogsApi(contextProvider);
			_userCatalogProvider = new UserCatalogProvider(contextProvider, stateProvider);
		}

		private IList<long> GetProductsToScore(long userId)
		{
			var userCatalog = _userCatalogProvider.Get(userId);
			if (userCatalog == null)
				return new long[0];

			return userCatalog.Items.Any() ? userCatalog.Items.Select(x => x.Id).ToArray() : new long[0];
		}

		public IList<ProductScore> CalculateForUser(long userId)
		{
			var productsToScore = GetProductsToScore(userId).Select(x => new ProductScore { ProductId = x, Score = 0 }).ToArray();
			if (!productsToScore.Any())
				return new ProductScore[0];

			var productsCount = CountFriendsProducts(userId);
			if (!productsCount.Any())
				return productsToScore;

			foreach (var product in productsToScore.Where(product => productsCount.ContainsKey(product.ProductId)))
			{
				product.Score = productsCount[product.ProductId];
			}

			return productsToScore;
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
	}

	public class ProductScore
	{
		public long ProductId { get; set; }
		public decimal Score { get; set; }
	}
}
