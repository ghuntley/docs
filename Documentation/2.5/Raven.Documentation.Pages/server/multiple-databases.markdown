# Multiple Databases

RavenDB natively supports multiple databases, and like most things in RavenDB, the way you configure additional databases is by creating a document. The RavenDB multi database support was explicitly designed to support multi tenancy scenarios, and RavenDB can easily handle hundreds or thousands of databases on the same instance.

**Note:** Multiple databases are available only over HTTP, if you are running RavenDB in embedded mode, you need to manage multiple databases yourself.

Defining a new database is done by creating a document with the name "Raven/Databases/[database name]" with the following contents:

{CODE-START:json /}
	// Raven/Databases/Northwind
	{
        "Settings" : 
        { 
              "Raven/DataDir": "~/Tenants/Northwind"
        }
    }
{CODE-END /}

The Settings dictionary allows you to modify the configuration for RavenDB's for the specified database. The list of available configuration options can be found [here](administration/configuration#availability-of-configuration-options).

Once that document is created, you can access the Northwind database using the same REST based API, but with the following base endpoint:

    http://localhost:8080/databases/northwind

Everything else remains the same. Note that unlike other databases, there isn't any additional steps that you have to go through. Once the document describing the database is created, RavenDB will create the database the first time a requests comes in for that database.

**Note:** You can access the WebUI for the specified database by browsing to: http://localhost:8080/databases/northwind

From the client API, you have the following options:

{CODE multiple_databases_1@Server\MultipleDatabases.cs /}

The first line will ensure that the database document exists, and the second will access the database. All operations done in the context of the northwindSession will apply only to the Northwind database.

## Bundles
All the bundles on the server will be available to all the databases on the server.

## Isolation
Different databases are completely isolated from one another, and there is no way for data from one database to leak to another database. Documents and attachments for each database are stored in separate physical locations. Indexes defined in each database can work only on data local to that database.

There is no way to share data between different databases on the same instance. From the point of view of RavenDB, you can treat each database on the server instance as a separate server. You can even setup replication between different databases on the same server instance.

## Backups
Each database has to be backed up independently.

## Working Set
RavenDB's databases were designed with multi tenancy in mind, and are meant to support large number of databases on a single server. In order to do that, RavenDB will only keep the active databases open. If you access a database for the first time, that database will be opened and started, so the next request to that database wouldn't have to pay the cost of opening the database. But if a database hasn't been accessed for a while, RavenDB will cleanup all resources associated with the database and close it.

That allows RavenDB to manage large numbers of databases, because at any given time, only the active databases are actually taking resources.