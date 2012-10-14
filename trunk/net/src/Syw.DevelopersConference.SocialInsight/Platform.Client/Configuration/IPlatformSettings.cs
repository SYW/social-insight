using System;

namespace Platform.Client.Configuration
{
	public interface IPlatformSettings
	{
		Uri SywWebSiteUrl { get; }
		string SywAppLoginUrl { get; }
		Uri ApiUrl { get; }
		Uri SecureApiUrl { get;  }
	}
}