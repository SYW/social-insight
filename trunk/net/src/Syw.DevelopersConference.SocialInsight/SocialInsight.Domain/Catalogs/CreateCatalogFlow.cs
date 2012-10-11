using SocialInsight.Domain.Context;

namespace SocialInsight.Domain.Catalogs
{
	public interface ICreateCatalogFlow
	{
		void Create();
	}

	public class CreateCatalogFlow : ICreateCatalogFlow
	{
		private readonly CatalogsRepository _catalogsRepository;
		private readonly IUserContextProvider _userContextProvider;
		private readonly CatalogsApi _catalogsApi;

		public CreateCatalogFlow()
		{
			_catalogsApi = new CatalogsApi();
			_userContextProvider = new UserContextProvider();
			_catalogsRepository = new CatalogsRepository();
		}

		public void Create()
		{
			var currentUserId = _userContextProvider.GetUserId();

			var userCatalog = _catalogsRepository.GetUserCatalog(currentUserId);

			if (userCatalog.HasValue)
				return;

			var catalogId = _catalogsApi.Create("Social Insight", 
												"Contains all the items the user wants to query in the Social Insight app",
												Privacy.Private);

			_catalogsRepository.AddUserCatalog(currentUserId, catalogId);
		}
	}
}
