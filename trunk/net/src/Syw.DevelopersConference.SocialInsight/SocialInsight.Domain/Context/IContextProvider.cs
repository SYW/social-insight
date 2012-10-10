using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialInsight.Domain.Context
{
	public interface IContextProvider
	{
		T Get<T>(string key);
		void Set<T>(string key, T value);
	}
}
