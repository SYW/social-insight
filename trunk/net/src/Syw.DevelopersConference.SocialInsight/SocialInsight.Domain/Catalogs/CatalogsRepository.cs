using System.Linq;
using SocialInsight.Domain.DataAccess;

namespace SocialInsight.Domain.Catalogs
{
	public class CatalogsRepository
	{
		private readonly SessionProvider _sessionProvider;

		public CatalogsRepository()
		{
			_sessionProvider = new SessionProvider();
		}

		public void AddUserCatalog(long userId, long catalogId)
		{
			_sessionProvider.WithSession(session => session.Save(new UserCatalogDto { UserId = userId, CatalogId = catalogId }));
		}

		public long? GetUserCatalog(long userId)
		{
			var userCatalog = _sessionProvider.Query<UserCatalogDto>(q => q.Where(x => x.UserId == userId)).FirstOrDefault();

			return userCatalog == null ? 
				(long?)null : 
				userCatalog.CatalogId;
		}

	}
}
