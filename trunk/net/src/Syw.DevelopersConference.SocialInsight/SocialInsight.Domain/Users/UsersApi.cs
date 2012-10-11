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

		public IList<long> GetFriends(long userId)
		{
			var userIdParameter = new KeyValuePair<string, object>("userId", userId);

			var followers = Proxy.Get<long[]>(GetEndpointPath("followers"), userIdParameter);
			var followedBy = Proxy.Get<long[]>(GetEndpointPath("followed-by"), userIdParameter);

			return followers.Join(followedBy, x => x, y => y, (x, y) => x).ToList();
		}
	}
}
