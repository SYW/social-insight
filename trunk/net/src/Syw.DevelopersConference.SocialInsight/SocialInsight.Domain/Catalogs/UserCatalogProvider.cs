using System;
using System.Linq;
using Platform.Client.Common.Context;

namespace SocialInsight.Domain.Catalogs
{
	public interface IUserCatalogProvider
	{
		CatalogDto Get(long userId);
	}

	public class UserCatalogProvider : IUserCatalogProvider
	{
		private readonly ICatalogsApi _catalogsApi;
		private readonly ICatalogsRepository _catalogsRepository;
		private readonly ICreateCatalogFlow _createCatalogFlow;

		public UserCatalogProvider(IContextProvider contextProvider,
			IStateProvider stateProvider)
		{
			_catalogsApi = new CatalogsApi(contextProvider);
			_catalogsRepository = new CatalogsRepository();
			_createCatalogFlow = new CreateCatalogFlow(contextProvider, stateProvider);
		}

		public CatalogDto Get(long userId)
		{
			var userCatalogId = _catalogsRepository.GetUserCatalog(userId);
			if (!userCatalogId.HasValue)
				throw new InvalidOperationException("Social Insight catalog was not created for user #" + userId);

			var userCatalog = _catalogsApi.Get(new[] { userCatalogId.Value }).FirstOrDefault();
			if (userCatalog == null)
			{
				_createCatalogFlow.Create();
				return null;
			}

			return userCatalog;
		}
	}
}
