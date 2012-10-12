using System.Threading.Tasks;
using Platform.Client;
using Platform.Client.Common.Context;
using SocialInsight.Domain.Auth;
using SocialInsight.Domain.Catalogs;
using SocialInsight.Domain.Products;
using System.Linq;

namespace SocialInsight.Domain.Dashboard
{
	public interface IPrecalculatedDashboardFlow
	{
		void CalculateForAllUsers();
	}

	public class PrecalculatedDashboardFlow : IPrecalculatedDashboardFlow
	{
		private readonly IDashboardFlow _dashboardFlow;
		private readonly IOfflineTokenProvider _offlineTokenProvider;
		private readonly ICatalogsRepository _catalogsRepository;
		private readonly IProductsRepository _productsRepository;
		private readonly IPlatformTokenProvider _platformTokenProvider;

		public PrecalculatedDashboardFlow(IContextProvider contextProvider, 
			IStateProvider stateProvider)
		{
			_dashboardFlow = new DashboardFlow(contextProvider, stateProvider);
			_offlineTokenProvider = new OfflineTokenProvider(contextProvider);
			_platformTokenProvider = new PlatformTokenProvider(contextProvider);
			_catalogsRepository = new CatalogsRepository();
			_productsRepository = new ProductsRepository();
		}

		public void CalculateForAllUsers()
		{
			var userCatalogs = _catalogsRepository.GetAllUserCatalogs();

			Parallel.ForEach(userCatalogs,
			                 CalculateInsightForUser);
		}

		private void CalculateInsightForUser(UserCatalogDto userCatalog)
		{
			SetToken(userCatalog.UserId);

			var scoredProducts = _dashboardFlow.GetScoredProducts();

			var productsInsight =
				scoredProducts.Where(x => x.Score > 0).Select(
					x => new ProductInsightDto {ProductId = x.Product.Id, UserId = userCatalog.UserId, Score = x.Score})
				.ToArray();

			_productsRepository.SaveProductInsights(productsInsight);
		}

		private void SetToken(long userId)
		{
			var token = _offlineTokenProvider.Get(userId);
			_platformTokenProvider.Set(token);
		}
	}
}
