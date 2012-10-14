using System.Collections.Generic;
using Platform.Client.Common.Context;

namespace SocialInsight.Domain.Wall
{
	public interface IWallApi
	{
		void PublishUserAction(string action, string title, string content, string imageUrl, string targetUrl);
	}

	public class WallApi : ApiBase, IWallApi
	{
		protected override string BasePath { get { return "wall"; } }

		public WallApi(IContextProvider contextProvider) : base(contextProvider)
		{}

		public void PublishUserAction(string action, string title, string content, string imageUrl, string targetUrl)
		{
			Call<string>("publish-user-action",
			             new KeyValuePair<string, object>("userAction", action),
			             new KeyValuePair<string, object>("title", title),
			             new KeyValuePair<string, object>("content", content),
			             new KeyValuePair<string, object>("imageUrl", imageUrl),
			             new KeyValuePair<string, object>("targetUrl", targetUrl));
		}
	}
}
