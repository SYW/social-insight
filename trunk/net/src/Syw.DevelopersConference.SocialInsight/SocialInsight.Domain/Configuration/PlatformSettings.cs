using System;
using Platform.Client.Configuration;

namespace SocialInsight.Domain.Configuration
{
	public class PlatformSettings : IPlatformSettings
	{
		public Uri SywWebSiteUrl { get { return Config.GetUri("platform:syw-site-url"); } }
		public string SywAppLoginUrl { get { return Config.GetString("platform:syw-app-login-url"); } }
		public Uri ApiUrl { get { return Config.GetUri("platform:api-url"); } }
		public Uri SecureApiUrl { get { return Config.GetUri("platform:secured-api-url"); } }
	}
}
