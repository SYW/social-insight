using System.Collections.Generic;

namespace SocialInsight.Domain.AppLinks
{
	public interface IAppLinksApi
	{
		void CreateAppLink(string title, string url);
	}

	public class AppLinksApi : ApiBase, IAppLinksApi
	{
		protected override string BasePath { get { return "applinks"; } }

		public void CreateAppLink(string title, string url)
		{
			Call<string>("register",
			             new KeyValuePair<string, object>("title", title),
			             new KeyValuePair<string, object>("url", url));
		}
	}
}
