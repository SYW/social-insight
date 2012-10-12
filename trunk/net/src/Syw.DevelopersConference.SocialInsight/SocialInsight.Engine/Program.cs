using Platform.Client.Common.Context;
using SocialInsight.Domain.Dashboard;

namespace SocialInsight.Engine
{
	class Program
	{
		static void Main()
		{
			var precalculatedDashboardFlow = new PrecalculatedDashboardFlow(new ThreadContextProvider(),
			                                                                new ThreadStateProvider());

			precalculatedDashboardFlow.CalculateForAllUsers();
		}
	}
}
