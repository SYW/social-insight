using System;
using System.Configuration;

namespace SocialInsight.Domain.Configuration
{
	public class ApplicationSettings
	{
		public long AppId { get { return Config.GetLong("app:id"); } }
		public string AppSecret { get { return Config.GetString("app:secret"); } }
	}
}
