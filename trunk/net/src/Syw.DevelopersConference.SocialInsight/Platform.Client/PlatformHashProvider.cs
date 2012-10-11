using System.Security.Cryptography;
using System.Text;
using Platform.Client.Common;
using Platform.Client.Configuration;

namespace Platform.Client
{
	public interface IPlatformHashProvider
	{
		string GetHash();
	}

	public class PlatformHashProvider : IPlatformHashProvider
	{
		private static readonly SHA256 HashAlgorithm = SHA256.Create();
		private readonly IApplicationSettings _applicationSettings;
		private readonly IPlatformTokenProvider _platfromTokenProvider;

		public PlatformHashProvider(IApplicationSettings applicationSettings,
			IPlatformTokenProvider platformTokenProvider)
		{
			_applicationSettings = applicationSettings;
			_platfromTokenProvider = platformTokenProvider;
		}

		public string GetHash()
		{
			var saltedWithSecret = Encoding.UTF8.GetBytes(_platfromTokenProvider.Get() + _applicationSettings.AppSecret);

			return HashAlgorithm.ComputeHash(saltedWithSecret).ToHexString().ToLower();
		}
	}
}
