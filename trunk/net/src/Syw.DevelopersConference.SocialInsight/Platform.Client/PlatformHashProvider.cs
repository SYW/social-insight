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
		private readonly SHA256 _hashAlgorithm;
		private readonly IApplicationSettings _applicationSettings;
		private readonly IPlatformTokenProvider _platfromTokenProvider;

		public PlatformHashProvider(IApplicationSettings applicationSettings,
			IPlatformTokenProvider platformTokenProvider)
		{
			_hashAlgorithm = SHA256.Create();
			_applicationSettings = applicationSettings;
			_platfromTokenProvider = platformTokenProvider;
		}

		public string GetHash()
		{
			var saltedWithSecret = Encoding.UTF8.GetBytes(_platfromTokenProvider.Get() + _applicationSettings.AppSecret);

			return _hashAlgorithm.ComputeHash(saltedWithSecret).ToHexString().ToLower();
		}
	}
}
