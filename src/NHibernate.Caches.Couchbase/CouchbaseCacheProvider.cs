using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using NHibernate.Cache;
using Couchbase;
using Couchbase.Configuration;
using System;

namespace NHibernate.Caches.Couchbase
{
	/// <summary>
	/// Cache provider using the .NET client (http://github.com/enyim/EnyimMemcached)
	/// for memcached, which is located at http://memcached.org/
	/// </summary>
	public class CouchbaseCacheProvider : ICacheProvider
	{
		private static readonly IInternalLogger log;
		private static CouchbaseClient clientInstance;
		private static readonly ICouchbaseClientConfiguration config;
		private static readonly object syncObject = new object();

		static CouchbaseCacheProvider()
		{
			log = LoggerProvider.LoggerFor(typeof (CouchbaseCacheProvider));
			config = ConfigurationManager.GetSection("couchbase") as ICouchbaseClientConfiguration;
			if (config == null)
			{
				log.Info("couchbase configuration section not found, using default configuration (127.0.0.1:8091).");
				config = new CouchbaseClientConfiguration();
				config.Urls.Add(new UriBuilder("http://", IPAddress.Loopback.ToString(),8091, "pools").Uri);
			}
		}

		#region ICacheProvider Members

		public ICache BuildCache(string regionName, IDictionary<string, string> properties)
		{
			if (regionName == null)
			{
				regionName = "";
			}
			if (properties == null)
			{
				properties = new Dictionary<string, string>();
			}
			if (log.IsDebugEnabled)
			{
				var sb = new StringBuilder();
				foreach (var pair in properties)
				{
					sb.Append("name=");
					sb.Append(pair.Key);
					sb.Append("&value=");
					sb.Append(pair.Value);
					sb.Append(";");
				}
				log.Debug("building cache with region: " + regionName + ", properties: " + sb);
			}
			return new CouchbaseCacheClient(regionName, properties, clientInstance);
		}

		public long NextTimestamp()
		{
			return Timestamper.Next();
		}

		public void Start(IDictionary<string, string> properties)
		{
			// Needs to lock staticly because the pool and the internal maintenance thread
			// are both static, and I want them syncs between starts and stops.
			lock (syncObject)
			{
				if (config == null)
				{
					throw new ConfigurationErrorsException("Configuration for enyim.com/memcached not found");
				}
				if (clientInstance == null)
				{
					clientInstance = new CouchbaseClient(config);
				}
			}
		}

		public void Stop()
		{
			lock (syncObject)
			{
				clientInstance.Dispose();
				clientInstance = null;
			}
		}

		#endregion
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