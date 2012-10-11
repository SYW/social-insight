using System.Collections.Generic;
using System.Linq;

namespace SocialInsight.Domain.Catalogs
{
	public interface ICatalogsApi
	{
		long Create(string catalogName, string description, Privacy privacy);
		IList<CatalogDto> Get(IList<long> ids);
		IList<CatalogDto> GetUsersCatalogs(IList<long> userIds);
	}

	public class CatalogsApi : ApiBase, ICatalogsApi
	{
		protected override string BasePath { get { return "catalogs"; } }

		public long Create(string catalogName, string description, Privacy privacy)
		{
			var catalogId = Call<long>("create",
										new KeyValuePair<string, object>("name", catalogName),
										new KeyValuePair<string, object>("description", description),
										new KeyValuePair<string, object>("privacy", privacy));

			return catalogId;
		}

		public IList<CatalogDto> Get(IList<long> ids)
		{
			return Call<IList<CatalogDto>>("get", new KeyValuePair<string, object>("ids", ids));
		}

		public IList<CatalogDto> GetUsersCatalogs(IList<long> userIds)
		{
			var userCatalogIds = userIds
					.SelectMany(x => Call<IList<long>>("get-user-catalogs", new KeyValuePair<string, object>("userId", x)))
					.Distinct().ToArray();

			return Get(userCatalogIds);
		}
	}
}
