using Platform.Client.Common.Context;
using SocialInsight.Domain.Users;

namespace SocialInsight.Domain.Context
{
	public interface IUserContextProvider
	{
		long GetUserId();
	}

	public class UserContextProvider : IUserContextProvider
	{
		private const string UserIdKey = "user-id";

		private readonly IUsersApi _usersApi;
		private readonly CookieContextProvider _contextProvider;

		public UserContextProvider()
		{
			_usersApi = new UsersApi();
			_contextProvider = new CookieContextProvider();
		}

		public long GetUserId()
		{
			var userId = _contextProvider.Get(UserIdKey);

			long id;

			if (!string.IsNullOrEmpty(userId) && long.TryParse(userId, out id))
				return id;

			id = _usersApi.GetCurrentUser().Id;
			_contextProvider.Set(UserIdKey, id.ToString());

			return id;
		}
	}
}
