using FluentNHibernate.Mapping;

namespace SocialInsight.Domain.Catalogs
{
	public class UserCatalogDtoMap : ClassMap<UserCatalogDto>
	{
		public UserCatalogDtoMap()
		{
			Not.LazyLoad();
			Table("user_catalogs");

			Id(x => x.UserId).GeneratedBy.Assigned();
			Map(x => x.CatalogId);
		}
	}
}
