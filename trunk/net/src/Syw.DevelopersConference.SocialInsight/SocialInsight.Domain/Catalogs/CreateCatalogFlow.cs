using System;
using System.Linq;
using Platform.Client.Common.Context;
using Platform.Client.Common.WebClient;
using SocialInsight.Domain.Context;

namespace SocialInsight.Domain.Catalogs
{
	public interface ICreateCatalogFlow
	{
		void Create();
	}

	public class CreateCatalogFlow : ICreateCatalogFlow
	{
		private const string CatalogName = "Social Insight";
		private readonly CatalogsRepository _catalogsRepository;
		private readonly IUserContextProvider _userContextProvider;
		private readonly CatalogsApi _catalogsApi;

		public CreateCatalogFlow(IContextProvider contextProvider,
			IStateProvider stateProvider)
		{
			_catalogsApi = new CatalogsApi(contextProvider);
			_userContextProvider = new UserContextProvider(stateProvider, contextProvider);
			_catalogsRepository = new CatalogsRepository();
		}

		public void Create()
		{
			var currentUserId = _userContextProvider.GetUserId();

			var userCatalog = _catalogsRepository.GetUserCatalog(currentUserId);

			if (userCatalog.HasValue)
				return;

			long catalogId = 0;

			try
			{
				catalogId = _catalogsApi.Create(CatalogName,
												"Contains all the items the user wants to query in the Social Insight app",
												Privacy.Private);
			}
			catch (RequestException ex)
			{
				if (ex.StatusCode == 400) // meaning the catalog already exists
					catalogId = TryFindSocialInsightCatalog(currentUserId);
			}

			_catalogsRepository.AddUserCatalog(currentUserId, catalogId);
		}

		private long TryFindSocialInsightCatalog(long userId)
		{
			var userCatalogs = _catalogsApi.GetUsersCatalogs(new[] {userId});

			var socialInsightCatalog = userCatalogs.FirstOrDefault(x => x.Name == CatalogName);

			if (socialInsightCatalog == null)
				throw new InvalidOperationException("Unable to find or create the Social Insight catalog");

			return socialInsightCatalog.Id;
		}
	}
}
