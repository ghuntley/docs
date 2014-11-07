﻿# Transaction support in RavenDB

All the previous examples have assumed that a single unit of work can be achieved with a single IDocumentSession and a single call to `SaveChanges`, and for the most part this is definitely true. Sometimes however we do need multiple calls to `SaveChanges` for one reason or another, but we want those calls to be contained within a single atomic operation.

RavenDB supports `System.Transactions` for multiple operations against a RavenDB server, or even against multiple RavenDB servers.

The client code for this is as simple as::

{CODE transaction_support_1@ClientApi\Advanced\TransactionSupport.cs /}
	
If at any point any of this code fails, none of the changes will be enacted against the RavenDB document store.

The implementation details of this are not important, although it is possible to see that RavenDB does indeed send a transaction Id along with all of the the HTTP requests under this transaction scope as shown here:

    POST /bulk_docs HTTP/1.1
    Raven-Transaction-Information: 975ee0bf-cac9-4b8e-ba29-377de722f037, 00:01:00
    Accept-Encoding: deflate,gzip
    Content-Type: application/json; charset=utf-8
    Host: 127.0.0.1:8081
    Content-Length: 300
    Expect: 100-continue

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

A call to commit involves a separate call to another HTTP endpoint with that transaction id:

	POST /transaction/commit?tx=975ee0bf-cac9-4b8e-ba29-377de722f037 HTTP/1.1
	Accept-Encoding: deflate,gzip
	Content-Type: application/json; charset=utf-8
	Host: 127.0.0.1:8081
	Content-Length: 0

{NOTE While RavenDB supports `System.Transactions`, you should only use this if you really require this (for example, to coordinate between multiple transactional resources), since there is additional cost for using `System.Transactions` and distributed transactions over simply using the standard API and the transactional `SaveChanges`. /}
