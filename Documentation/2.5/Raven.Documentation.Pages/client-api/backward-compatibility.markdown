﻿# Backward Compatibility

## Client v1 to Server v2

Once you upgrade your from version 1 to version 2, you may still have a production application which still make use of the client DLLs of version 1. For the most part, the client of version 1 is compatible with server 2, expect for the following cases:

* **Facets:**	the implementation of facets was change is version 2, and it's not compatible with the Client v1.
* **Reduce with Average or Count:**	Using the `.Average()` or `.Count()` extension methods in the reduce statements is not allowed in v2, since it produces inconsistent results.

So if you do not use one of the above cases, you can safely upgrade your server to v2 without needing to upgrade all of your applications to use the v2 of the client, since the v1 of the client should be working fine the v2 of the server.

## Client v2 to Server v2.5

The only breaking change that we have between v2 client and v2.5 server is if you are using the DTC feature and used promotable transactions.

If you aren't using promotable transactions (for example, you use MSMQ & RavenDB together), there is no issue. If you require promotable transactions, you will have to use the v2.5 client.