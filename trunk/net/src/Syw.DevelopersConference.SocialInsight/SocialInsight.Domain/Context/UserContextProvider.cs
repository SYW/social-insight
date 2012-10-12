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
		private readonly IStateProvider _stateProvider;

		public UserContextProvider(
			IStateProvider stateProvider, 
			IContextProvider contextProvider)
		{
			_usersApi = new UsersApi(contextProvider);
			_stateProvider = stateProvider;
		}

		public long GetUserId()
		{
			var userId = _stateProvider.Get(UserIdKey);

			long id;

			if (!string.IsNullOrEmpty(userId) && long.TryParse(userId, out id))
				return id;

			id = _usersApi.GetCurrentUser().Id;
			_stateProvider.Set(UserIdKey, id.ToString());

			return id;
		}
	}
}
