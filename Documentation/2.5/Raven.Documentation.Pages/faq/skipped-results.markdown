# SkippedResults

When querying RavenDB, you sometimes get a result that contained skipped results. What are those? And why do we care? Let us assume that we have the following index:

    from img in docs.Images
    from tag in img.Tags
    select new { tag }

And you issue the following query:

    /indexes/ImagesByTag?query=tag:NoSQL

Each image may have multiple tags, so it may have multiple results in the index. Here is an example of the actual physical index structure:

    { "__document_id": "imgs/1", "tag": "RavenDB" } 
    { "__document_id": "imgs/1", "tag": "NoSql" } 
    { "__document_id": "imgs/2", "tag": "NoSQL" } 
    { "__document_id": "imgs/2", "tag": "NoSql" } 
    { "__document_id": "imgs/3", "tag": "Databases" } 

As you can see, we have several documents that contains multiple results for the same document.

Now, the query above is going to return the follow results from the index:

    { "__document_id": "imgs/1", "tag": "NoSql" } 
    { "__document_id": "imgs/2", "tag": "NoSQL" } 
    { "__document_id": "imgs/2", "tag": "NoSql" } 

Note that imgs/2 appears twice in the result set, however, when we are querying for documents, there isn't really a point in returning the same document twice (and it drastically increase the response size), so we filter it out and return each document only once.

When SkippedResults is greater than 0 it implies that we skipped over some results in the index because they represent a document that we already load. We have to report this information to the client, because it is an important factor when paging. You starting point is (pageSize * currentPage + **SkippedResults**), not just (pageSize * currentPage).