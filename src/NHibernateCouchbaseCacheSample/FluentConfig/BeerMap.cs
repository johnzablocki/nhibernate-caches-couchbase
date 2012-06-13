using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernateCouchbaseCacheSample.Models;
using FluentNHibernate.Mapping;

namespace NHibernateCouchbaseCacheSample.FluentConfig
{
	public class BeerMap : ClassMap<Beer>
	{
		public BeerMap()
		{
			Table("Beers");
			Cache.ReadWrite();

			Id(b => b.Id);
			Map(b => b.Name).Not.Nullable().Length(30);
			Map(b => b.Brewery).Not.Nullable().Length(50);
			Map(b => b.ABV).Not.Nullable();
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