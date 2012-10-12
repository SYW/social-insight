using FluentNHibernate.Mapping;

namespace SocialInsight.Domain.Products
{
	public class ProductInsightDtoMap : ClassMap<ProductInsightDto>
	{
		public ProductInsightDtoMap()
		{
			Not.LazyLoad();
			Table("products_insight");

			CompositeId()
				.KeyProperty(x => x.UserId)
				.KeyProperty(x => x.ProductId);

			Map(x => x.Score);
		}
	}
}
