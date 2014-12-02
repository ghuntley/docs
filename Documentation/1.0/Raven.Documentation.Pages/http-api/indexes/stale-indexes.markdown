#Handling stale indexes

As part of the response when an index is queried, a property is attached indicating whether the results are stale - that is, whether there are currently any tasks outstanding against that index.

{CODE-START:json /}
    > curl "http://localhost:8080/indexes/blogEntryByCategory?query=Category:RavenDb

    {
          "Results":[
                {
                      "Title":"Handling Stale Indexes",
                      "Category":"RavenDb",
                      "ObjectType":"BlogEntry",
                      "@metadata":{
                            "Content-Type":"application/x-www-form-urlencoded",
                            "@id":"blogentries/1",
                            "@etag":"17b3f1fd-4c79-11df-8ec2-001fd08ec235"
                      }
                }
          ],
          "IsStale":true,
          "TotalResults":1
    }
{CODE-END /}

That's the **IsStale** property there, indicating that the results that have come back are stale. Presumably somebody has added a new blog entry and the index has not yet had time to be updated.

A valid strategy for retrieving non-stale results is to set a timeout and query the database until the timeout expires, or until non-stale results are returned

For example, we could wait five seconds and run that same query again to get the new blog entry now it's been indexed:

{CODE-START:json /}
    > curl "http://localhost:8080/indexes/blogEntryByCategory?query=Category:RavenDb

    {
          "Results":[
                {
                      "Title":"Handling Stale Indexes",
                      "Category":"RavenDb",
                      "ObjectType":"BlogEntry",
                      "@metadata":{
                            "Content-Type":"application/x-www-form-urlencoded",
                            "@id":"blogentries/1",
                            "@etag":"17b3f1fd-4c79-11df-8ec2-001fd08ec235"
                      }
                },
                {
                      "Title":"Another post on RavenDB",
                      "Category":"RavenDb",
                      "ObjectType":"BlogEntry",
                      "@metadata":{
                            "Content-Type":"application/x-www-form-urlencoded",
                            "@id":"blogentries/4",
                            "@etag":"3f29eef8-a8ed-410d-8f8c-5a0667c94efc"
                      }
                },
          ],
          "IsStale":true,
          "TotalResults":2
    }
{CODE-END /}
    
As you can see though, the IsStale flag is still true, because in the mean time somebody else has added yet another blog entry.

To work around this, a 'cutOff' parameter may be included, which will set the IsStale flag based on all the tasks against an index up to a certain point in time.

{CODE-START:json /}
    > curl "http://localhost:8080/indexes/blogEntryByCategory?query=Category:RavenDb&cutOff=2010-05-16T12:31:25.8017940%2B01:00
    
    {
          "Results":[
                {
                      "Title":"Handling Stale Indexes",
                      "Category":"RavenDb",
                      "ObjectType":"BlogEntry",
                      "@metadata":{
                            "Content-Type":"application/x-www-form-urlencoded",
                            "@id":"blogentries/1",
                            "@etag":"17b3f1fd-4c79-11df-8ec2-001fd08ec235"
                      }
                },
                {
                      "Title":"Another post on RavenDB",
                      "Category":"RavenDb",
                      "ObjectType":"BlogEntry",
                      "@metadata":{
                            "Content-Type":"application/x-www-form-urlencoded",
                            "@id":"blogentries/4",
                            "@etag":"3f29eef8-a8ed-410d-8f8c-5a0667c94efc"
                      }
                },
          ],
          "IsStale":false,
          "TotalResults":2
    }
{CODE-END /}

This time, the same results came back because the third blog entry still hasn't been indexed - but IsStale is false because the cut-off period was specified and happens to be before that third blog entry was added to the database.

A better strategy, is therefore to get the current date and time on the first request, and for subsequent queries, use that current date and time to avoid further modifications keeping the index stale.