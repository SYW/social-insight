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

		public WallApi(IContextProvider contextProvider)
			: base(contextProvider)
		{ }

		public void PublishUserAction(string action, string title, string content, string imageUrl, string targetUrl)
		{
			Call<string>("publish-user-action", new
			{
				UserAction = action,
				Title = title,
				Content = content,
				ImageUrl = imageUrl,
				TargetUrl = targetUrl
			});
		}
	}
}
