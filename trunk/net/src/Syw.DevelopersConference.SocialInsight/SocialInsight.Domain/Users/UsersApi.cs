using Platform.Client;
using Platform.Client.Common.Context;
using SocialInsight.Domain.Configuration;

namespace SocialInsight.Domain.Users
{
	public interface IUsersApi
	{
		UserDto GetCurrentUser();
	}

	public class UsersApi : ApiBase, IUsersApi
	{
		protected override string BasePath { get { return "users"; } }

		public UserDto GetCurrentUser()
		{
			return Proxy.Get<UserDto>(GetEndpointPath("current"));
		}
	}
}
