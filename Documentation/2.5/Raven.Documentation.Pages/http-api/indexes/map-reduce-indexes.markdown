#HTTP API - Map Reduce Indexes

Indexes enable advanced fast and complex queries of JSON documents already in RavenDB. Indexes are expressed using LINQ and are composed of up to two parts, a map and a reduce function.

This section of the documentation focuses on map reduce indexes. Map reduce indexes allow for complex aggregation of data by first selecting records (map) and then applying the specified reduce function to these records to produce a smaller set of results.

For a more in-depth look at how map reduce works, read this post: [Map / Reduce a Visual Explanation.](http://ayende.com/blog/4435/map-reduce-a-visual-explanation)

##Creating a Map Reduce Index

Map reduce indexes are created in the same way as simple indexes, just with a reduce function specified as well:

{CODE-START:json /}
curl -X PUT http://localhost:8080/indexes/byEmail -d "{ Map: 'from doc in docs where doc.email != null select new {doc.email, count = 1 };' ,
     Reduce: 'from result in results group result by result.email into g select new { email = g.Key, count = g.Sum(x=>x.count)  } '}"
{CODE-END /}

On a successful index create, RavenDB will respond with a HTTP 201 Created response code, and a JSON acknowledgment of the index just created:

{CODE-START:json /}
HTTP/1.1 201 Created
&nbsp;
{"index":"byEmail"}
{CODE-END /}

##Querying a Map Reduce Index

Querying a Map Reduce is done the same way as with a simple index. Assuming we've previously added a document such as:

{CODE-START:json /}
curl http://localhost:8080/docs/ayende1 -X PUT "{ _id: 'ayende',email: 'ayende@ayende.com', projects: [ 'rhino mocks','nhibernate','rhino service bus','raven db' ] }"
curl http://localhost:8080/docs/ayende2 -X PUT "{ _id: 'ayende',email: 'ayende@ayende.com', projects: [ 'rhino mocks','nhibernate','rhino service bus','raven db' ] }"
{CODE-END /}

Then a query of the "byEmail" map reduce index would look like:

{CODE-START:json /}
curl -X GET http://localhost:8080/indexes/byEmail?query=email:ayende@ayende.com
&nbsp;
{"Results":[{"email":"ayende@ayende.com","count":"2"}],"IsStale":false,"TotalResults":1}
{CODE-END /}

##Paging Results & Deleting

Paging results and deleting a map reduce index are accomplished the same way as for a simple index.