using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Linq;
using SocialInsight.Domain.Catalogs;

namespace SocialInsight.Domain.DataAccess
{
	public class SessionProvider
	{
		private static readonly ISessionFactory SessionFactory = Create();

		private static ISessionFactory Create()
		{
			return Fluently.Configure()
				.Database(MySQLConfiguration.Standard.ConnectionString(x => x.FromConnectionStringWithKey("MySql")))
				.Mappings(x => x.FluentMappings.AddFromAssemblyOf<UserCatalogDto>())
				.BuildSessionFactory();
		}

		public void WithSession(Action<ISession> action)
		{
			using(var session = SessionFactory.OpenSession())
			{
				action(session);
				session.Flush();				
			}
		}

		public IList<T> Query<T>(Func<IQueryable<T>, IQueryable<T>> query)
		{
			IList<T> result;

			using(var session = SessionFactory.OpenSession())
			{
				result = query(session.Query<T>()).ToList();
			}

			return result;
		}
	}
}
