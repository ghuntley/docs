# HiLo algorithm

The HiLo algorithm is an efficient solution used by [a session](../session/what-is-a-session-and-how-does-it-work) to generate numeric parts of identifiers. In other words it is
responsible for providing numeric values that are combined with collection names to create keys like `orders/10`, `products/93` etc. 

The client is able to determine to what type of the collection does an entity belong. In order to differ documents, it also needs to add a unique number at the end of the document's key.
To ensure that multiple clients can generate the keys simultaneously, they need some mechanism to avoid duplicates. It is ensured with `Replication/HiLo/collection` documents, stored 
in a database, which are modified by the clients. These documents have a very simple construction:

{CODE-BLOCK:json}
{
    "Max": 32
}
{CODE-BLOCK/}

The `Max` property means the maximum possible number that has been used by any client to create the key for a given collection. It is used as follows:

1. The client gets the HiLo document and reads `Max` value.
2. Then it adds a number (called capacity) to `Max`.
3. It updates the document by the new `Max` value and puts it into the database.
4. If someone else modified this document concurrently, the client gets `ConcurrencyException` and needs to repeat the process.

This way the client is able to generate a range of numbers it can use to generate identifiers.

## Replication scenario

The usage of [a replication](../../server/scaling-out/replication/how-replication-works) doesn't influence the algorithm of a document ID generation. However in a Master/Master replication scenario it might be useful 
to add a server specific prefix to generated document identifiers. This would help to protect against conflicts of the document IDs between the replicating servers. 
In order to set up the server's prefix, you have to put `Raven/ServerPrefixForHilo` document:

{CODE replication_hilo@ClientApi\DocumentIdentifiers\HiLoAlgorithm.cs /}

The ServerPrefix value will be fetch in the same request as the current HiLo document and will become a part of a generated document identifier as well.
 For example, storing the first `Order` object will make `orders/NorthServer/1` its key.