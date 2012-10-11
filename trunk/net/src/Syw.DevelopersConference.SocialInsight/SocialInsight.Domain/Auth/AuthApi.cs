namespace SocialInsight.Domain.Auth
{
	public interface IAuthApi
	{
		UserState GetUserState();
	}

	public class AuthApi : ApiBase, IAuthApi
	{
		protected override string BasePath { get { return "auth"; } }

		public UserState GetUserState()
		{
			return Proxy.Get<UserState>(GetEndpointPath("user-state"));
		}
	}
}
