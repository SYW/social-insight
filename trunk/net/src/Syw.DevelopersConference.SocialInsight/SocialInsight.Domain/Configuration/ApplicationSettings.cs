using System;
using System.Configuration;

namespace SocialInsight.Domain.Configuration
{
	public class ApplicationSettings
	{
		public long AppId { get { return Convert.ToInt64(ConfigurationManager.AppSettings["app:id"]); } }
		public string AppSecret { get { return ConfigurationManager.AppSettings["app:secret"]; } }
	}
}
