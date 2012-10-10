using System.Configuration;

namespace SocialInsight.Domain.Configuration
{
	public class PlatformSettings
	{
		public string SywWebSiteUrl { get { return ConfigurationManager.AppSettings["platform:syw-site-url"]; } }
		public string SywAppLoginUrl { get { return ConfigurationManager.AppSettings["platform:syw-app-login-url"]; } }
		public string ApiUrl { get { return ConfigurationManager.AppSettings["platform:api-url"]; } }
	}
}
