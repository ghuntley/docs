﻿#What is a collection?

A collection in RavenDB is a set of documents with the same tag. The tag is defined in `Raven-Entity-Name` metadata which is filled in by the client based on the type of 
an entity object that you store (the function responsible for tagging documents can by overwritten by using [customizations](../../client-api/configuration/conventions/identifier-generation/global#findtypetagname-and-finddynamictagname)). Note that documents that are in 
the same collection can have a completely different structure, what is fine because RavenDB is schema-less.

The collection is just a virtual concept. There is no influence on how or where the documents within the same collection are stored. However the collection concept
has a great meaning for three RavenDB features: [the studio](../../studio/overview/documents/documents-view), [the indexes](../../indexes/what-are-indexes) and [the document key generation](../../client-api/document-identifiers/working-with-document-ids) on the client side.

##Collections usage

###Studio

Probably the first time you encounter the collection will be accessing the studio. Then you will see that, for example, the `Order` entity that you have just stored is visible under 
`Orders` collection (by default the client pluralizes the collection name based on the type name). But how does it happen that the virtual concept of the collections is
visualized in the studio. The answer is that each RavenDB database has the built-in [`Raven/DocumentsByEntityName`](../../indexes/indexing-basics#default-index) index, which allows to query the database to retrieve
only  documents from the specified collection. This way the studio can group the documents into the collections.


###Indexing

Another usage of collections is filtering documents during indexing process. When you create an index you determine what collection does it involve. During indexing process only the documents
that belong this collection are indexed, the rest is filtered out.

###Document keys

The default convention is that documents have the identifiers like `orders/1` where `orders` is just a collection name and `1` is the identity value. 
Also the ranges of available identity values returned by [HiLo algorithm](../../client-api/document-identifiers/hilo-algorithm) are per the collection name.