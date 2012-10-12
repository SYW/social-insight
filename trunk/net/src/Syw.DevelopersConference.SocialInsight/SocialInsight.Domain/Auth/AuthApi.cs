using System;
using System.Collections.Generic;
using Platform.Client.Common.Context;

namespace SocialInsight.Domain.Auth
{
	public interface IAuthApi
	{
		UserState GetUserState();
		string GetOfflineToken(long userId, long appId, DateTime timestamp, string signature);
	}

	public class AuthApi : ApiBase, IAuthApi
	{
		public AuthApi(IContextProvider contextProvider) : base(contextProvider)
		{
		}

		protected override string BasePath { get { return "auth"; } }

		public UserState GetUserState()
		{
			return Call<UserState>("user-state");
		}

		public string GetOfflineToken(long userId, long appId, DateTime timestamp, string signature)
		{
			return Proxy.SecuredGetWithoutContext<string>(GetEndpointPath("get-token"),
			                                              new KeyValuePair<string, object>("userId", userId),
			                                              new KeyValuePair<string, object>("appId", appId),
														  // Timestamp is only valid without miliseconds. see documantation
			                                              new KeyValuePair<string, object>("timestamp",
			                                                                               new DateTime(timestamp.Year,
			                                                                                            timestamp.Month,
			                                                                                            timestamp.Day,
			                                                                                            timestamp.Hour,
			                                                                                            timestamp.Minute,
			                                                                                            timestamp.Second)),
			                                              new KeyValuePair<string, object>("signature", signature));
		}
	}
}
