using System.Collections.Generic;
using System.Linq;
using SocialInsight.Domain.DataAccess;

namespace SocialInsight.Domain.Products
{
	public class ProductsRepository
	{
		private readonly SessionProvider _sessionProvider;

		public ProductsRepository()
		{
			_sessionProvider = new SessionProvider();
		}

		public IList<ProductInsightDto> GetProductInsightForUser(long userId)
		{
			return _sessionProvider.Query<ProductInsightDto>(q => q.Where(x => x.UserId == userId));
		}

		public void SaveProductInsights(IList<ProductInsightDto> insights)
		{
			_sessionProvider.WithSession(s => s.SaveOrUpdate(insights));
		}
	}
}
