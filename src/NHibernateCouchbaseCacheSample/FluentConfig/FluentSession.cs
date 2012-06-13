using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernateCouchbaseCacheSample.Models;
using NHibernate.Caches.Couchbase;
using NHibernateCouchbaseCacheSample.Modules;

namespace NHibernateCouchbaseCacheSample.FluentConfig
{
	public class FluentSession
	{
		public static ISessionFactory CreateSessionFactory()
		{
			string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|Beers.mdf;Integrated Security=True;User Instance=True";

			return Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
				.Cache(c => c.UseQueryCache().ProviderClass<CouchbaseCacheProvider>())
				.Mappings(m =>
					m.FluentMappings.AddFromAssemblyOf<Beer>())
				.ExposeConfiguration(c => c.SetProperty("current_session_context_class", "web"))
				.BuildSessionFactory();
		}

		public static ISession GetCurrentSession()
		{
			return NHibernateSessionPerRequest.GetCurrentSession();
		}
	}
}

#region [ License information          ]
/* ************************************************************
 * 
 *    @author Couchbase <info@couchbase.com>
 *    @copyright 2012 Couchbase, Inc.
 *    
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *    
 *        http://www.apache.org/licenses/LICENSE-2.0
 *    
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *    
 * ************************************************************/
#endregion