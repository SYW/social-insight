using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
		public CatalogsApi() : base(new HttpContextProvider())
		{
		}

		protected override string BasePath { get { return "catalogs"; } }

		public long Create(string catalogName, string description, Privacy privacy)
		{
			return Call<long>("create",
								new KeyValuePair<string, object>("name", catalogName),
								new KeyValuePair<string, object>("description", description),
								new KeyValuePair<string, object>("privacy", privacy));
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
