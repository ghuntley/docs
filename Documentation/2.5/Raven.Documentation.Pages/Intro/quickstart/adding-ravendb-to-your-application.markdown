# Adding RavenDB to your application

There are two flavors of RavenDB available:

* **RavenDB Client**: this is a lightweight library enabling you to connect to any RavenDB server; it exposes all RavenDB's strengths to your .NET or Silverlight application.

* **RavenDB Embedded**: this is a full-blown RavenDB server ready to be embedded in your application.

If you are not sure which to choose, go with the RavenDB Client. Only use Embedded if you are certain this is indeed what you need.

{NOTE RavenDB Embedded cannot be run on the Client Profile}

Either of these can be added to your project by either using **NuGet** or extracting the contents of a build package.

## Installing using NuGet

You can consume RavenDB through NuGet. Follow [this](http://ravendb.net/docs/intro/quickstart/adding-ravendb-through-nuget) article for instructions on how to do this.

## Manually adding RavenDB using the build package

Grab a stable build from the [downloads page](http://ravendb.net/download), and extract the required files to a new "RavenDB" folder in your application. You'll then need to update your project with references to them.

Here's how to know which of the files you are going to need:

![The folder structure in a RavenDB build package](images/build_package.png)

* /Client - Lightweight RavenDB client for .NET 4.0. **This is the recommended client to use**.

* /Silverlight - A lightweight Silverlight 5 client for RavenDB and its dependencies.

* /EmbeddedClient - The files required to run the RavenDB client, in server or embedded mode.

Whichever client version you choose to use, reference all the assemblies in the corresponding folder to your project.

As for the rest of the folders in the package, here's a brief description of what they contain:

* /Backup - [Standalone backup tool](../../server/administration/backup-restore), for performing backup operations using a user with admin privileges.

* /Bundles - [Bundles](../../server/extending/bundles) that extend RavenDB in various ways.

* /Samples - Some sample applications for Raven. Under each sample application folder there is a "Start Raven.cmd" file which will starts Raven with all the data and indexes required to run the sample successfully.

* /Smuggler - [The Import/Export utility](../../server/administration/export-import) for RavenDB.

* /Server - The files required to run RavenDB in server / service mode. Execute /Server/Raven.Server.exe /install to register and start the RavenDB service.

* /Web - The files required to run RavenDB under IIS. Create an IIS site in the /Web directory to start the RavenDB site.

For more information on the various deployment options for RavenDB, see in the [Deployment section of the chapter on the Server side](../../server/deployment).
