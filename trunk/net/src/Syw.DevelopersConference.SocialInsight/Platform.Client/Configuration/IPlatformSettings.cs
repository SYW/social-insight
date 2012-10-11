using System;

namespace Platform.Client.Configuration
{
	public interface IPlatformSettings
	{
		string SywWebSiteUrl { get; }
		string SywAppLoginUrl { get; }
		Uri ApiUrl { get; }
	}
}