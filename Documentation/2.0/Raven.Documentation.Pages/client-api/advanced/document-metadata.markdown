﻿# Working with document metadata

Each RavenDB document has metadata properties attached to it, which are being used internally but can be exposed to your code. This is usually used for things _about_ the entity, which aren't a direct part of it.

A consumer may choose to use those properties (for indexing) or add key/value pairs of his own should he need to.

## Internal metadata keys

Here is a list of all properties RavenDB stores as metadata for its documents:

* **Raven-Clr-Type** - Records the CLR type, set and used by the JSON serialization/deserialization process in the Client API.
* **Raven-Entity-Name** - Records the entity name, aka the name of the RavenDB collection this entity belongs to.
* **Non-Authoritive-Information** - This boolean value will be set to true if the data received by the client has been modified by an uncommitted transaction. You can read more on it in the Advanced section.
* **Temp-Index-Score** - When querying RavenDB, this value is the [Lucene score](http://lucene.apache.org/java/2_9_2/scoring.html) of the entity for the query that was executed.
* **Raven-Read-Only** - This document should be considered read only and not modified.
* **Last-Modified** - The last modified time-stamp for the entity.
* **@etag** - Every document in RavenDB has a corresponding e-tag (entity tag) stored as a sequential Guid. This e-tag is updated by RavenDB every time the document is changed.
* **@id** - The entity id, as extracted from the entity itself.

More metadata keys are used for storing replication information, concurrency bookkeeping and ACL used for securing the entity.

## Using the metadata

Getting the metadata for an entity is quite easy. From the Client API, you simply call `Advanced.GetMetadataFor` on the entity, like so:

{CODE getting_metadata@ClientApi\Advanced\DocumentMetadata.cs /}

The `RavenJObject` is simply a dictionary, and the Client API provides convenient ways to transform the values in it to different types.

Metadata can be easily used when querying, too. For example, to create an index returning all entities by their entity name, you can use the following Map function:

    from doc in docs
    where doc["@metadata"]["Raven-Entity-Name"] != null
    select new { Tag = doc["@metadata"]["Raven-Entity-Name"] };

Using the HTTP API, some metadata is sent as headers when getting and manipulating a single document, and as a JSON document embedded in the entity's document (inside the `@metadata` element) when dealing with multiple documents.

## Manipulating metadata

While metadata was originally meant to be used by RavenDB itself, it is also possible to add your own data to the metadata. This is useful when you have data about the entity you want to store, and it doesn't make sense to store it within the entity itself.

There are two ways of doing that:

* **Explicitly** - after retrieving a `RavenJObject` holding the metadata by calling `session.Advanced.GetMetadatFor(entity)` method call, you can explicitly manipulate that object and add keys of your own. Those changes will be tracked in the Unit of Work, and persisted following the next call to SaveChanges.

* **Listeners** - `documentStore.RegisterListener(myStoreListener)` provide a way to register an implementation of `IDocumentStoreListener`, which will be called when any session is about to store the entity to RavenDB.

In each of those approaches, you get an access to the metadata instance, which you can then manipulate for your own needs. Common reasons to do this include wanting to add additional information for indexes to index, communicating with a server side bundle or simply having an out of band channel to store additional information about a document.