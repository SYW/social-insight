using SocialInsight.Domain.Context;

namespace SocialInsight.Domain.Platform
{
	public class PlatformToken
	{
		private const string TokenKey = "token";
		private readonly IContextProvider _contextProvider;

		public PlatformToken(IContextProvider contextProvider)
		{
			_contextProvider = contextProvider;
		}

		public void Set(string token)
		{
			_contextProvider.Set(TokenKey, token);
		}

		public string Get()
		{
			return _contextProvider.Get<string>(TokenKey);
		}
	}
}
