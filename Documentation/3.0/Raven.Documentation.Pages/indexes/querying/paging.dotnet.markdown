﻿# Paging

Paging, or pagination, is the process of splitting a dataset into pages, reading one page at a time. This is useful for optimizing bandwidth traffic, optimizing hardware usage, or just because no user can handle huge amounts of data at once anyway.

{SAFE If not specified, page size **on client side** is set to **128**. /}

{SAFE If not specified, maximum page size **on server side** is set to **1024** and can be altered using `Raven/MaxPageSize` setting (more information [here](../../server/configuration/configuration-options)). /}

## Example I - safe by default

All of the bellow queries will return up to **128** results due to the client default page size value.

{CODE-TABS}
{CODE-TAB:csharp:Query paging_0_1@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:DocumentQuery paging_0_2@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Commands paging_0_3@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Index paging_0_4@Indexes\Querying\Paging.cs /}
{CODE-TABS/}

All of the bellow queries will return up to **1024** results due to the server default max page size value.

{CODE-TABS}
{CODE-TAB:csharp:Query paging_1_1@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:DocumentQuery paging_1_2@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Commands paging_1_3@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Index paging_0_4@Indexes\Querying\Paging.cs /}
{CODE-TABS/}

## Example II - basic paging

Let's assume that our page size is `10` and we want to retrieve 3rd page. To do this we need to issue following query:

{CODE-TABS}
{CODE-TAB:csharp:Query paging_2_1@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:DocumentQuery paging_2_2@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Commands paging_2_3@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Index paging_0_4@Indexes\Querying\Paging.cs /}
{CODE-TABS/}

## Finding the total results count when paging

While paging you sometimes need to know the exact number of results returned from the query. The Client API supports this explicitly:

{CODE-TABS}
{CODE-TAB:csharp:Query paging_3_1@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:DocumentQuery paging_3_2@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Commands paging_3_3@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Index paging_0_4@Indexes\Querying\Paging.cs /}
{CODE-TABS/}

While the query will return with just 10 results, `totalResults` will hold the total number of matching documents.

## Paging through tampered results

For some queries, server will skip over some results internally, and by that invalidate the `TotalResults` value e.g. when executing a Distinct query or index produces multiple index entries per document, `TotalResults` will contain the total count of matching documents found, but will not take into account results that were skipped as a result of the `Distinct` operator.

Whenever `SkippedResults` is greater than 0 it implies that we skipped over some results in the index.
    
In order to do proper paging in those scenarios, you should use the `SkippedResults` when telling RavenDB how many documents to skip. In other words, for each page the starting point should be `.Skip((currentPage * pageSize) + SkippedResults)`.

For example, let's page through all the results:

{CODE-TABS}
{CODE-TAB:csharp:Query paging_4_1@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:DocumentQuery paging_4_2@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Commands paging_4_3@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Index paging_0_4@Indexes\Querying\Paging.cs /}
{CODE-TABS/}

{CODE-TABS}
{CODE-TAB:csharp:Query paging_6_1@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:DocumentQuery paging_6_2@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Commands paging_6_3@Indexes\Querying\Paging.cs /}
{CODE-TAB:csharp:Index paging_6_0@Indexes\Querying\Paging.cs /}
{CODE-TABS/}

## Increasing StartsWith performance

All `StartsWith` operations (e.g. [LoadStartingWith](../../client-api/session/loading-entities#loadstartingwith) and [Stream](../../client-api/session/querying/how-to-stream-query-results) from advanced session operations or [StartsWith](../../client-api/commands/documents/get#startswith) and [Stream](../../client-api/commands/documents/stream) from low-level commands) contain a `RavenPagingInformation` parameter that can be used to increase the performance of a StartsWith operation when **next page** is requested.

To do this we need to pass same instance of `RavenPagingInformation` to the identical operation. The client will use information contained in this object to increase the performance (only if next page is requested).

{CODE paging_5_1@Indexes\Querying\Paging.cs /}

## Related articles

- [Indexing : Basics](../../indexes/indexing-basics)
- [Querying : Basics](../../indexes/querying/basics)
- [Querying : Filtering](../../indexes/querying/filtering)
- [Querying : Sorting](../../indexes/querying/sorting)
