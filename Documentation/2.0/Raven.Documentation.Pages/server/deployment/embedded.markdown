# Embedded

RavenDB makes it very easy to be embedded within your application. The first step is to reference the embedded client, either via nuget (package name: RavenDB-Embedded) or by taking the files from the build zip.

After referencing the embedded client from your project, all that is left to do is initializing:

{CODE embedded1@Server\Deployment\Embedded.cs /}

## HTTP access

By default you don't have an external access to RavenDB, so if you want to use the WebUI to look at what the database is doing, or to use the REST API, you can't. Other features relying on being able to communicate over HTTP (like replication) will be disabled too.

RavenDB can be run in an embedded mode with HTTP enabled. To do that, you will just need to set another flag when initializing the embedded document store:

{CODE embedded2@Server\Deployment\Embedded.cs /}

Note that you may want to call `NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(port)` to ensure that you can open the HTTP server without requiring administrator privileges.

Once you initialized the document store, you can browse directly to the WebUI, execute replication scenarios, etc.

## Configuration

Many configuration options are available for tuning RavenDB and fitting it to your needs. See the [Configuration options](http://ravendb.net/docs/server/administration/configuration?version=2.0) page for complete info.