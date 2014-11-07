# Error handling in Indexes

Indexes in RavenDB are user provided Linq queries running on top of dynamic JSON data model. There is a wide space for errors here, either because of malformed index definition or missing / corrupt data on the JSON document itself.

## Index compilation errors

An index definition such as the following one will fail:

{CODE-START:json /}
    { "Map" : "from doc in docs where doc.Type == 'posts' select new{ doc.Title.Length }" }
{CODE-END /}

The error is the use of single quotes to enclose a string, something that is not allowed in C#. This will result in the following compilation error:

{CODE-START:json /}
    {
           "url":"/indexes/PostsByTitle",
           "error":"System.InvalidOperationException: Could not understand query: \r\n-- line 1 col 44: Char not terminated\r\n-- line 1 col 50: Char not terminated\r\n-- line 1 col 47: 
                              invalid QueryExpressionBody\r\n\r\n     at Raven.Database.Linq.QueryParsingUtils.GetVariableDeclaration(String query)"
    }
{CODE-END /}

The error isn't very clear in the JSON format, but what it is saying is:

    Could not understand query: 
       -- line 1 col 44: Char not terminated
       -- line 1 col 50: Char not terminated
       -- line 1 col 47:   invalid QueryExpressionBody
    
Which should give you enough information to figure out what is wrong. Those errors are immediate, and require no further action from the database. The only thing that the user can do is fix the index definition.

## Index execution errors

A common case is an index that doesn't take into account that other documents also exists on the server. For example, let us take this index:

{CODE-START:json /}
    { "Map" : "from doc in docs select new{ doc.Title.Length }" }
{CODE-END /}

This index make the assumption that all documents have a Title property. A document that doesn't have that property will return null when it is access, resulting in a NullReferenceException when the index is executed.

Because indexes are updated on a background thread, it is unlikely that users will be aware of those errors.  

RavenDB surfaces index execution errors in two places, the first is the database statistics. Accessible for programmatic access at '/stats' or in human readable form at '/raven/studio.html#/statistics' or '/raven/statistics.html'

The following is the output of the '/stats' endpoint:  

{CODE-START:json /}
	{
		"LastDocEtag": "00000000-0000-0b00-0000-000000000001",
		"LastAttachmentEtag": "00000000-0000-0000-0000-000000000000",
		"CountOfIndexes": 1,
		"ApproximateTaskCount": 0,
		"CountOfDocuments": 1,
		"StaleIndexes": [],
		"CurrentNumberOfItemsToIndexInSingleBatch": 512,
		"CurrentNumberOfItemsToReduceInSingleBatch": 256,
		"Indexes":[
			{
				"Name": "PostsByTitle",
				"IndexingAttempts": 1,
				"IndexingSuccesses": 0,
				"IndexingErrors": 1
			}
		],
		"Errors":[
			{
				"Index": "PostsByTitle",
				"Error": "Cannot   perform   runtime   binding   on   a   null   reference",
				"Timestamp": "\/Date(1271778107096+0300)\/",
				"Document": "bob"
			}
		]
	}
{CODE-END /}

As you can see, RavenDB surfaces both the fact that the index has encountered, what was the document it errored on,    and what that error was. The errors collection contains the last 50 errors that happened on the server.

In addition to that, the server logs may contain additional information regarding the error.

## Disabling an index

Furthermore, in order to protect itself from indexes that always fail, RavenDB will disable an index if it keeps failing. The actual logic for disabling an index is:

* If an index has 15% or more failure rate - it is disabled
* The 15% count is only considered after the first 10 documents (to avoid immediately disabling an index if the first document it indexes is invalid).

A disabled index cannot be queried, all queries to a disabled index will result in an error similar to this one:

{CODE-START:json /}
    {
             "url":"/indexes/PostsByTitle",
             "error":"Index   PostsByTitle   is   invalid,   out   of   10   indexing   attempts,   10   has   failed.\r\nError   rate   of   100%   exceeds   allowed   15%   error   rate",
             "index":"PostsByTitle"
    }
{CODE-END /}

The only thing that can be done with a disabled index is to either delete it or replace the index definition with one that is resilient to those errors.

There is no way to "resume" a disabled index.