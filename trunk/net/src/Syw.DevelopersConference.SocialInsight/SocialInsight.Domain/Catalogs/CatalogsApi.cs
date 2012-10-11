using System.Collections.Generic;

namespace SocialInsight.Domain.Catalogs
{
	public interface ICatalogsApi
	{
		long Create(string catalogName, string description, Privacy privacy);
	}

	public class CatalogsApi : ApiBase, ICatalogsApi
	{
		protected override string BasePath { get { return "catalogs"; } }

		public long Create(string catalogName, string description, Privacy privacy)
		{
			var catalogId = Proxy.Get<long>(GetEndpointPath("create"),
			                                new KeyValuePair<string, object>("name", catalogName),
			                                new KeyValuePair<string, object>("description", description),
			                                new KeyValuePair<string, object>("privacy", privacy));

			return catalogId;
		}
	}
}
