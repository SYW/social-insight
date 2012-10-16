using System.Collections.Generic;
using System.Linq;
using Platform.Client.Common.Context;

namespace SocialInsight.Domain.Users
{
	public interface IUsersApi
	{
		UserDto GetCurrentUser();
		IList<long> GetFriends(long userId);
	}

	public class UsersApi : ApiBase, IUsersApi
	{
		private const string CurrentUserCacheKey = "users:current";

		private readonly IContextProvider _context;

		protected override string BasePath { get { return "users"; } }

		public UsersApi(IContextProvider contextProvider)
			: base(contextProvider)
		{
			_context = contextProvider;
		}

		public UserDto GetCurrentUser()
		{
			var user = _context.Get<UserDto>(CurrentUserCacheKey);

			if (user == null)
			{
				user = Call<UserDto>("current");
				_context.Set(CurrentUserCacheKey, user);
			}

			return user;
		}

		public IList<long> GetFriends(long userId)
		{
			var userIdParameter = new { UserId = userId };

			var followers = Call<long[]>("followers", userIdParameter);
			var followedBy = Call<long[]>("followed-by", userIdParameter);

			return followers.Join(followedBy, x => x, y => y, (x, y) => x).ToList();
		}
	}
}
