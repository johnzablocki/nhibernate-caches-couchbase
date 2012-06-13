using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernateCouchbaseCacheSample.FluentConfig;
using NHibernate.Context;

namespace NHibernateCouchbaseCacheSample.Modules
{
	public class NHibernateSessionPerRequest : IHttpModule
	{

		private static readonly ISessionFactory _sessionFactory;

		static NHibernateSessionPerRequest()
		{
			_sessionFactory = FluentSession.CreateSessionFactory();
		}

		public void Init(HttpApplication context)
		{

			context.BeginRequest += BeginRequest;
			context.EndRequest += EndRequest;

		}

		public static ISession GetCurrentSession()
		{
			return _sessionFactory.GetCurrentSession();
		}

		public void Dispose() { }

		private static void BeginRequest(object sender, EventArgs e)
		{

			ISession session = _sessionFactory.OpenSession();
			session.BeginTransaction();
			CurrentSessionContext.Bind(session);

		}

		private static void EndRequest(object sender, EventArgs e)
		{

			ISession session = CurrentSessionContext.Unbind(_sessionFactory);
			if (session == null) return;
			try
			{
				session.Transaction.Commit();
			}

			catch (Exception)
			{
				session.Transaction.Rollback();
			}
			finally
			{
				session.Close();
				session.Dispose();
			}
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

//from http://bengtbe.com/blog/2009/10/08/nerddinner-with-fluent-nhibernate-part-3-the-infrastructure/