using System.Collections.Generic;
using System.Linq;
using SocialInsight.Domain.DataAccess;

namespace SocialInsight.Domain.Products
{
	public interface IProductsRepository
	{
		IList<ProductInsightDto> GetProductInsightForUser(long userId);
		void SaveProductInsights(IList<ProductInsightDto> insights);
	}

	public class ProductsRepository : IProductsRepository
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
			_sessionProvider.WithSession(s =>
				{
					foreach (var insight in insights)
					{
						s.SaveOrUpdate(insight);
					}
				});
		}
	}
}
