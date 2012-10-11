using SocialInsight.Domain.Users;

namespace SocialInsight.Domain.Catalogs
{
	public interface ICreateCatalogFlow
	{
		void Create();
	}

	public class CreateCatalogFlow : ICreateCatalogFlow
	{
		private readonly CatalogsApi _catalogsApi;
		private readonly IUsersApi _usersApi;
		private readonly CatalogsRepository _catalogsRepository;

		public CreateCatalogFlow()
		{
			_catalogsApi = new CatalogsApi();
			_usersApi = new UsersApi();
			_catalogsRepository = new CatalogsRepository();
		}

		public void Create()
		{
			var currentUserId = _usersApi.GetCurrentUser().Id;

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
