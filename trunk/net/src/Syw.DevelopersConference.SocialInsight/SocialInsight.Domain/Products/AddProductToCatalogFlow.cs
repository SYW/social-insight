using System.Linq;
using Platform.Client.Common.Context;
using SocialInsight.Domain.Catalogs;
using SocialInsight.Domain.Context;

namespace SocialInsight.Domain.Products
{
	public class CatalogItemsFlow
	{
		private readonly IUserCatalogProvider _userCatalogProvider;
		private readonly ICatalogItemsApi _catalogItemsApi;
		private readonly IUserContextProvider _userContextProvider;

		public CatalogItemsFlow(IContextProvider contextProvider,
			IStateProvider stateProvider)
		{
			_userContextProvider = new UserContextProvider(stateProvider, contextProvider);
			_userCatalogProvider = new UserCatalogProvider(contextProvider, stateProvider);
			_catalogItemsApi = new CatalogItemsApi(contextProvider);
		}

		public bool AddProduct(long productId)
		{
			var userId = _userContextProvider.GetUserId();
			var userCatalog = _userCatalogProvider.Get(userId);

			if (userCatalog.Items != null &&
				userCatalog.Items.Any(x => x.Id == productId))
				return false;

			_catalogItemsApi.AddProductToCatalogs(new[] { userCatalog.Id }, productId);

			return true;
		}
	}
}
