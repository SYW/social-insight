using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialInsight.Domain.Auth
{
	public enum UserState
	{
		Anonymous = 0,
		Authenticated = 1,
		Authorized = 2
	}
}
