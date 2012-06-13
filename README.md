nhibernate-caches-couchbase
===========================

2nd level caching provider for NHibernate using Couchbase.  

This provider is a port of the Enyim.Caching provider that is part of NHContrib - http://sourceforge.net/projects/nhcontrib/.  

#Usage

Once configured, the Couchbase NHibernate 2nd level cache should be transparent.

##Configure the Couchbase .NET Client Library

    <section name="couchbase" type="Couchbase.Configuration.CouchbaseClientSection, Couchbase" />

    <couchbase>
      <servers bucket="default">
        <add uri="http://127.0.0.1:8091/pools" />
      </servers>    
    </couchbase> 

##Configure NHibernate

<section name="hibernate-configuration" type="NHibernate.Cfg.ConfigurationSectionHandler, NHibernate" />

    <hibernate-configuration xmlns="urn:nhibernate-configuration-2.2">
    <session-factory>
    <property name="connection.provider">NHibernate.Connection.DriverConnectionProvider, NHibernate</property>
    <property name="dialect">NHibernate.Dialect.MsSql2000Dialect</property>
    <property name="connection.driver_class">NHibernate.Driver.SqlClientDriver</property>
    <property name="connection.connection_string">
    Server=localhost;initial catalog=nhibernate;Integrated Security=SSPI
    </property>
    <property name="connection.isolation">ReadCommitted</property>
    <property name="cache.provider_class"> NHibernate.Caches.Couchbase.MemCacheProvider,NHibernate.Caches.Couchbase</property>
    </session-factory>
    </hibernate-configuration> 

##Using FluentNHibernate

        return Fluently.Configure()
        .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
        .Cache(c => c.UseQueryCache().ProviderClass<CouchbaseCacheProvider>())
        .Mappings(m =>
            m.FluentMappings.AddFromAssemblyOf<Beer>())
        .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "web"))
        .BuildSessionFactory();

#Defaults

Without a "couchbase" config section, the provider will connect to a node on localhost using the default bucket.