using System;
using Platform.Client.Common;
using Platform.Client.Common.Context;
using Platform.Client.Configuration;
using SocialInsight.Domain.Configuration;

namespace SocialInsight.Domain.Auth
{
	public interface IOfflineTokenProvider
	{
		string Get(long userId);
	}

	public class OfflineTokenProvider : IOfflineTokenProvider
	{
		private readonly IApplicationSettings _applicationSettings;
		private readonly IAuthApi _authApi;

		public OfflineTokenProvider(IContextProvider contextProvider)
		{
			_applicationSettings = new ApplicationSettings();
			_authApi = new AuthApi(contextProvider);
		}

		public string Get(long userId)
		{
			var now = DateTime.UtcNow;
			var timestamp = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

			var signature = new SignatureBuilder()
				.Append(userId)
				.Append(_applicationSettings.AppId)
				.Append(timestamp)
				.Append(_applicationSettings.AppSecret)
				.Create();

			return _authApi.GetOfflineToken(userId, _applicationSettings.AppId, timestamp, signature);
		}
	}
}