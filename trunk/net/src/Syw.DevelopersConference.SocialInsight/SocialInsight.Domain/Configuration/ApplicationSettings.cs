using Platform.Client.Configuration;

namespace SocialInsight.Domain.Configuration
{
	public class ApplicationSettings : IApplicationSettings
	{
		public long AppId { get { return Config.GetLong("app:id"); } }
		public string AppSecret { get { return Config.GetString("app:secret"); } }
		public string AppLinkTitle { get { return Config.GetString("app:link-title"); } }
		public string AppLink { get { return string.Format(Config.GetString("app:link"), AppId); } }
	}
}
