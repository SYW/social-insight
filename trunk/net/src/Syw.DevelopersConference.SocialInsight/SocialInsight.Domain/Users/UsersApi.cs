using Platform.Client.Common.Context;

namespace SocialInsight.Domain.Users
{
	public interface IUsersApi
	{
		UserDto GetCurrentUser();
	}

	public class UsersApi : ApiBase, IUsersApi
	{
		private const string CurrentUserCacheKey = "users:current";

		private readonly HttpContextProvider _context;

		protected override string BasePath { get { return "users"; } }

		public UsersApi()
		{
			_context = new HttpContextProvider();
		}

		public UserDto GetCurrentUser()
		{
			var user = _context.Get<UserDto>(CurrentUserCacheKey);

			if (user == null)
			{
				user = Proxy.Get<UserDto>(GetEndpointPath("current"));
				_context.Set(CurrentUserCacheKey, user);
			}

			return user;
		}
	}
}
