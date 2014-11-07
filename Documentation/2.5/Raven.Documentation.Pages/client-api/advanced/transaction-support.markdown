﻿# Transaction support in RavenDB

In RavenDB all actions performed on documents are fully ACID (Atomicity, Consistency, Isolation, Durability). 

The indexing mechanism is built on a BASE model (Basically Available, Soft state, Eventual consistency).

RavenDB also is able to perform distributed transactions by implementing two-phase commit (2PC) transactions.

## Standard transactions

RavenDB has a built-int transaction support. A storage engine used by RavenDB under the hood (Esent) supports ACID transactional model. 

### ACID for document operations

An each document operation or a batch of operations applied to a set of documents sent in a single HTTP request will execute in a single transaction. The ACID properties of RavenDB for standard transactions:

* _Atomicity_  - All operations are atomic. Either they succeed or fail, not midway operation. In particular, operations on multiple documents will all happen atomically, all the way or none at all.

* _Consistency and Isolation / Consistency of Scans_ - In a single transaction, all operations operate under snapshot isolation. Even if you access multiple documents, you'll get all of their state as it was in the beginning of the request.

* _Visibility_ - All transactions are immediately made available on commit. Thus, if a transaction is commit after updating two docs, you'll always see the updates to those two docs at the same time. (That is, you either see the updates to both, or you don't see the update to either one).

* _Durability_ - If an operation has completed successfully, it was fsync'ed to disk. Reads will never return any data that hasn't been flushed to disk.

All of these constraints are ensured when you call `SaveChanges` or any action that creates a HTTP request by using DatabaseCommands for example.

### BASE for query operations

The transaction model is different when indexes are involved, because indexes are BASE, not ACID. Then the following constraints are applied to query operations:

* _Basically Available_ - Index query results will be always available, but they might be stale.

* _Soft state_ - The state of the system could change over the time because it is needed some time to perform the indexing. This is an incremental operation the less documents remains to index, the more accurate index results we have.

* _Eventual consistency_ - The database will eventually become consistent once it stops receiving new documents and the indexing operation finishes.

## DTC transactions

Sometimes we need multiple calls to `SaveChanges` for one reason or another, but we want those calls to be contained within a single atomic operation. 
RavenDB supports `System.Transactions` for multiple operations against a RavenDB server, or even against multiple RavenDB databases and servers (distributed transactions).

The client code for this is as simple as:

{CODE transaction_support_1@ClientApi\Advanced\TransactionSupport.cs /}
	
As you can see DTC transaction can happen on multiple requests. If at any point any of this code fails, none of the changes will be enacted against the RavenDB document store. 

You can see that RavenDB does indeed send a transaction identifier and its timeout (<em>Raven-Transaction-Information</em>) along with all of the the HTTP requests under this transaction scope as shown here:

{CODE-START:json /}
    POST /bulk_docs HTTP/1.1
    Raven-Transaction-Information: 975ee0bf-cac9-4b8e-ba29-377de722f037, 00:01:00
    Accept-Encoding: gzip
    Content-Type: application/json; charset=utf-8
    Host: 127.0.0.1:8081
    Content-Length: 300
    Expect: 100-continue
&nbsp;
    [
      {
        "Key": "blogs/1",
        "Etag": null,
        "Method": "PUT",
        "Document": {
          "Title": "Some new title",
          "Category": null,
          "Content": null,
          "Comments": null
        },
        "Metadata": {
          "Raven-Entity-Name": "Blogs",
          "Raven-Clr-Type": "ConsoleApplication5.Blog, ConsoleApplication5",
          "@id": "blogs/1",
          "@etag": "00000000-0000-0200-0000-000000000002"
        }
      }
    ]
{CODE-END /}

A call `transaction.Complete()` involves separate requests to another HTTP endpoint with that transaction id. According to the 2PC implementation of the DTC protocol, a commit is a two-phase operation. 
Phase one called _Prepare_ involves the first request when the actual work is made (but the transaction is not committed yet):

{CODE-START:json /}
	POST /transaction/prepare?tx=975ee0bf-cac9-4b8e-ba29-377de722f037 HTTP/1.1
	Accept-Encoding: gzip
	Content-Type: application/json; charset=utf-8
	Host: 127.0.0.1:8081
	Content-Length: 0
{CODE-END /}

If the Prepare phase succeeded then the actual transaction commit is made by sending the request:

{CODE-START:json /}
	POST /transaction/commit?tx=975ee0bf-cac9-4b8e-ba29-377de722f037 HTTP/1.1
	Accept-Encoding: gzip
	Content-Type: application/json; charset=utf-8
	Host: 127.0.0.1:8081
	Content-Length: 0
{CODE-END /}

All the intermediate states are durable between requests of the DTC transaction, and any document that has been modified is locked for writes from another transaction. All other transactions will see the uncommitted state, until the transaction is uncommitted. Once the DTC transaction has been committed, standard transaction rules apply.

{NOTE While RavenDB supports `System.Transactions`, you should only use this if you really require this (for example, to coordinate between multiple transactional resources), since there is additional cost for using `System.Transactions` and distributed transactions over simply using the standard API and the transactional `SaveChanges`. /}
