using System.Collections.Generic;
using Platform.Client.Common.Context;

namespace SocialInsight.Domain.AppLinks
{
	public interface IAppLinksApi
	{
		void CreateAppLink(string title, string url);
	}

	public class AppLinksApi : ApiBase, IAppLinksApi
	{
		public AppLinksApi() : base(new HttpContextProvider())
		{
		}

		protected override string BasePath { get { return "applinks"; } }

		public void CreateAppLink(string title, string url)
		{
			Call<string>("register",
			             new KeyValuePair<string, object>("title", title),
			             new KeyValuePair<string, object>("url", url));
		}
	}
}
