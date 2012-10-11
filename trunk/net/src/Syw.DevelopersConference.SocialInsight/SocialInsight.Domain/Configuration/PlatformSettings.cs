using System;
using Platform.Client.Configuration;

namespace SocialInsight.Domain.Configuration
{
	public class PlatformSettings : IPlatformSettings
	{
		public string SywWebSiteUrl { get { return Config.GetString("platform:syw-site-url"); } }
		public string SywAppLoginUrl { get { return Config.GetString("platform:syw-app-login-url"); } }
		public Uri ApiUrl { get { return new Uri(Config.GetString("platform:api-url"), UriKind.Absolute); } }
	}
}
