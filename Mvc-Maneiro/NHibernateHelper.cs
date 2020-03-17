using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Mvc_Maneiro
{
    public class NHibernateHelper
    {
        private static ISessionFactory _session;
        public static ISession OpenSession()
        {

            return CreateSession().OpenSession();
        }

        public static ISessionFactory CreateSession()
        {

            if (_session != null)
                return _session;

            IPersistenceConfigurer configDB = (MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("MyConnectionString")).ShowSql());

            _session = Fluently.Configure().Database(configDB)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Models.Aluno>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false,false))
                .BuildSessionFactory();

            return _session;
        }



    }
}