using Platform.Client;
using Platform.Client.Common.Context;
using SocialInsight.Domain.Configuration;

namespace SocialInsight.Domain.Users
{
	public interface IUsersApi
	{
		UserDto GetCurrentUser();
	}

	public class UsersApi : IUsersApi
	{
		private const string BasePath = "users";

		private readonly PlatformProxy _proxy;
		
		public UsersApi()
		{
			_proxy = new PlatformProxy(new PlatformSettings(),
			                           new ApplicationSettings(),
			                           new PlatformTokenProvider(new HttpContextProvider()));
		}

		public UserDto GetCurrentUser()
		{
			return _proxy.Get<UserDto>(GetEndpointPath("current"));
		}

		private static string GetEndpointPath(string endpoint)
		{
			return string.Format("/{0}/{1}", BasePath, endpoint);
		}
	}
}
