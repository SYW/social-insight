using System.Collections.Generic;
using System.Linq;
using Platform.Client.Common.Context;

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
		public CatalogsApi(IContextProvider contextProvider)
			: base(contextProvider)
		{
		}

		protected override string BasePath { get { return "catalogs"; } }

		public long Create(string catalogName, string description, Privacy privacy)
		{
			return Call<long>("create", new
			{
				Name = catalogName,
				Description = description,
				Privacy = privacy
			});
		}

		public IList<CatalogDto> Get(IList<long> ids)
		{
			return Call<IList<CatalogDto>>("get", new { Ids = ids });
		}

		public IList<CatalogDto> GetUsersCatalogs(IList<long> userIds)
		{
			var userCatalogIds = userIds
					.SelectMany(x => Call<IList<long>>("get-user-catalogs", new { UserId = x }))
					.Distinct().ToArray();

			return Get(userCatalogIds);
		}
	}
}
