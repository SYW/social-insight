using System.Security.Cryptography;
using System.Text;
using SocialInsight.Domain.Common;
using SocialInsight.Domain.Configuration;
using SocialInsight.Domain.Context;

namespace SocialInsight.Domain.Platform
{
	public class PlatformHashProvider
	{
		private readonly SHA256 _hashAlgorithm;
		private readonly ApplicationSettings _applicationSettings;
		private readonly PlatformToken _platfromToken;

		public PlatformHashProvider()
		{
			_hashAlgorithm = SHA256.Create();
			_applicationSettings = new ApplicationSettings();
			_platfromToken = new PlatformToken(new HttpContextProvider());
		}

		public string GetHash()
		{
			var saltedWithSecret = Encoding.UTF8.GetBytes(_platfromToken.Get() + _applicationSettings.AppSecret);

			return _hashAlgorithm.ComputeHash(saltedWithSecret).ToHexString().ToLower();
		}
	}
}
