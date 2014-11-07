# Exporting and Importing data

In order to export or import data from a RavenDB server, you can use the Raven.Smuggler utility.

Raven.Smuggler is distributed in both the:
- RavenDB [distribution package](http://ravendb.net/download). It is located under the `/Smuggler` folder.
- RavenDB.Server [nuget package](https://nuget.org/packages/RavenDB.Server). It is located under the `/tools` folder.

Using the Smuggler utility is necessary when trying to move a RavenDB Data folder around between servers. Simply copying it is not supported and can result in server errors.

## Exporting

To Export data, use this command:

    Raven.Smuggler out http://localhost:8080 dump.ravendump

This command will export all indexes, documents and attachments from the local RavenDB instance to a file named `dump.ravendump`.

The dump file will also include documents that were added during the export process, so you can make changes while the export is executing.

From `RavenDB 2.5` the Smuggler is using document streaming to speed up the process. To maintain backward compatibility, the Smuggler will detect from what version it exports the documents and adjust behavior accordingly.

Note that if you're using the replication bundle active on the database, it is recommend that you filter out the document with the ID `Raven/Replication/Destinations`, using the following command: `Raven.Smuggler out http://localhost:8080 dump.ravendump --negative-metadata-filter:@id=Raven/Replication/Destinations`.

## Importing

    Raven.Smuggler in http://localhost:8080 dump.ravendump

This command will import all the indexes, documents and attachments from the file to the local instance. 

{NOTE This will _overwrite_ any existing document on the local instance. /}

You can continue using that RavenDB instance while data is being imported to it.

To speed up the process, the `Raven.Smuggler.exe` is using [bulk inserts](../../client-api/advanced/bulk-inserts) and `The Studio` is using batching.

Note that if you have either the replication bundle or the periodic backup bunlde active on the database, it is recommened that you'll filter out the following documents when doing an import: `Raven/Replication/Destinations`, `Raven/Replication/VersionHilo`, `Raven/Backup/Periodic/Setup`, `Raven/Backup/Periodic/Status`.  
This can be done using the following command: `Raven.Smuggler in http://localhost:8080 dump.ravendump --negative-metadata-filter:@id=Raven/Replication/Destinations --negative-metadata-filter:@id=Raven/Backup/Periodic/Setup --negative-metadata-filter:@id=Raven/Backup/Periodic/Status --negative-metadata-filter:@id=Raven/Replication/VersionHilo`.

## Incremental Export and Import
With the incremental export operation we can use in order to backup the database incrementally, on each export, we will only take the export the documents create or updated
since the last export.

To export data with incremental we can use 2 options.  
If it is the first run and the folder does not exist yet use (you can continue to use this command every time):

    Raven.Smuggler out http://localhost:8080 folder_location --incremental

If you ran the command before or you created the folder you can use:

    Raven.Smuggler out http://localhost:8080 folder_location


In order to import date that was exported with incremental operation, you can use either of the following:

    1) Raven.Smuggler in http://localhost:8080 folder_location --incremental
    2) Raven.Smuggler in http://localhost:8080 folder_location

## Command line options

You can tweak the export/import process with the following parameters:

 - operate-on-types: Specify the types to export/import. Usage example: `--operate-on-types=Indexes,Documents,Attachments,Transformers`.
 - filter: Filter documents by a document property. Usage example: `--filter=Property-Name=Value`.
 - negative-filter: Filter documents NOT matching a document property. Usage example: `--negative-filter=Property-Name=Value`.   
 - metadata-filter: Filter documents by a metadata property. Usage example: `--metadata-filter=Raven-Entity-Name=Posts`.
 - negative-metadata-filter: Filter documents NOT matching a metadata property. Usage example: `--negative-metadata-filter=Raven-Entity-Name=Posts`.
 - transform: Transform documents using a given script (import only).   
 - transform-file: Transform documents using a given script file (import only).   
 - timeout: The timeout (in milliseconds) to use for requests.
 - batch-size: The batch size for requests.
 - database: The database to operate on. If no specified, the operations will be on the default database.
 - username: The username to use when the database requires the client to authenticate.
 - password: The password to use when the database requires the client to authenticate.
 - domain: The domain to use when the database requires the client to authenticate.
 - api-key: The API-key to use, when using OAuth.
 - incremental: States usage of incremental operations.
 - wait-for-indexing: Wait until all indexing activity has been completed (import only).
 - excludeexpired: Excludes expired documents created by the [expiration bundle](../extending/bundles/expiration).    
 - help: You can use the help option in order to print the built-in options documentation.

## Filtering

To filter out documents we introduced few filtering options that can be used during import or export process.

1. `filter` is used to filter documents based on a property. E.g. if we want to export all documents with property `Name` and value `John` then we must apply command as follows: `--filter=Name=John` .   
2. `negative-filter` is an opposite to `filter` and will filter documents that does NOT match the given property.  
3. `metadata-filter` is similar to `filter`, but works on document metadata properties.   
4. `negative-metadata-filter` filters out documents that does NOT match given metadata property.   

## Transforms

Transforms can be used to modify or filter out documents, but only work during the import process. The scripts must use JavaScript syntax and be in following format:   

{CODE-START:json /}
function(doc) {
	// custom code here
}
{CODE-END /}

where `doc` will contain our document with it's metadata under `@metadata` property.

#### Change scripts:   

E.g. To change document property `Name` value to the new one, the following script can be used:   

{CODE-START:json /}
function(doc) {
	doc['Name'] = 'NewValue';
	return doc;
}
{CODE-END /}

#### Filter scripts:    

If we return `null` then the document will be filtered out.   

{CODE-START:json /}
function(doc) {
	var id = doc['@metadata']['@id'];
	if(id === 'orders/999')
		return null;
	&nbsp;
	return doc;
}
{CODE-END /}

## SmugglerApi

Alternatively, if you prefer to do export/import from code rather than from the console utility, you can use the SmugglerApi class. In order to use this class you need to reference the Raven.Smuggler.exe.

Usage example:

{CODE smuggler-api@Server\Administration\ExportImport.cs /}

In the above code we exporting all of the data on the server, which is the documents, indexes, attachments and transformers, and than importing just the documents and the indexes. In this example the import would overwrite the existing documents, so if you want to import to another database you'll need to create another instance of the SmugllerApi with a different connection string options.