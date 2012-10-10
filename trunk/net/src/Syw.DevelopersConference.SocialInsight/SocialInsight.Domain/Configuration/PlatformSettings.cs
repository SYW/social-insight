using System.Configuration;

namespace SocialInsight.Domain.Configuration
{
	public class PlatformSettings
	{
		public string SywWebSiteUrl { get { return Config.GetString("platform:syw-site-url"); } }
		public string SywAppLoginUrl { get { return Config.GetString("platform:syw-app-login-url"); } }
		public string ApiUrl { get { return Config.GetString("platform:api-url"); } }
	}
}
