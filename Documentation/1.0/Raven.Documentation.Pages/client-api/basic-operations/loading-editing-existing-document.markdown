﻿# Loading & Editing an existing document

Each _document_ is stored as part of a _collection_, where a _collection_ is a set of documents sharing the same entity type.

Therefore, if you have the id of an existing document (for example the previously saved BlogPost entry), it can be loaded in the following manner:

{CODE editing_document_1@Intro\BasicOperations.cs /}

This results in the HTTP communication shown below (prettified for clarity):

    GET /docs/BlogPosts/1 HTTP/1.1
    Accept-Encoding: deflate,gzip
    Content-Type: application/json; charset=utf-8
    Host: 127.0.0.1:8080

    HTTP/1.1 200 OK
    Content-Type: application/json; charset=utf-8
    Last-Modified: Tue, 16 Nov 2010 20:37:01 GMT
    ETag: 00000000-0000-0100-0000-000000000002
    Server: Microsoft-HTTPAPI/2.0
    Raven-Entity-Name: Blogs
    Raven-Clr-Type: Blog
    Date: Tue, 16 Nov 2010 20:39:41 GMT
    Content-Length: 214

    {
      "Title": "Hello RavenDB",
      "Category": "RavenDB",
      "Content": "This is a blog about RavenDB",
      "Comments": [
        {
          "Title": "Unrealistic",
          "Content": "This example is unrealistic"
        },
        {
          "Title": "Nice",
          "Content": "This example is nice"
        }
      ]
    }

Changes can then be made to that object in the usual manner:

{CODE editing_document_2@Intro\BasicOperations.cs /}
	
Flushing those changes to the document store is achieved in the usual way:

{CODE editing_document_3@Intro\BasicOperations.cs /}
	
You don't have to call an `Update` method, or track any changes yourself. RavenDB will do all of that for you.

For the above example, the above example will result in the following HTTP message:

    POST /bulk_docs HTTP/1.1
    Accept-Encoding: deflate,gzip
    Content-Type: application/json; charset=utf-8
    Host: 127.0.0.1:8080
    Content-Length: 501
    Expect: 100-continue

    [
      {
        "Key": "BlogPosts/1",
        "Etag": null,
        "Method": "PUT",
        "Document": {
          "Title": "Some new title",
          "Category": "RavenDB",
          "Content": "This is a blog about RavenDB",
          "Comments": [
            {
              "Title": "Unrealistic",
              "Content": "This example is unrealistic"
            },
            {
              "Title": "Nice",
              "Content": "This example is nice"
            }
          ]
        },
        "Metadata": {
          "Content-Encoding": "gzip",
          "Raven-Entity-Name": "Blogs",
          "Raven-Clr-Type": "Blog",
          "Content-Type": "application/json; charset=utf-8",
          "@etag": "00000000-0000-0100-0000-000000000002"
        }
      }
    ]	
    
    
    HTTP/1.1 200 OK
    Content-Type: application/json; charset=utf-8
    Server: Microsoft-HTTPAPI/2.0
    Date: Tue, 16 Nov 2010 20:39:41 GMT
    Content-Length: 280

    [
      {
        "Etag": "00000000-0000-0100-0000-000000000003",
        "Method": "PUT",
        "Key": "BlogPosts/1",
        "Metadata": {
          "Content-Encoding": "gzip",
          "Raven-Entity-Name": "Blogs",
          "Raven-Clr-Type": "Blog",
          "Content-Type": "application/json; charset=utf-8",
          "@id": "BlogPosts/1"
        }
      }
    ]
	
{NOTE The entire document is sent to the server with the Id set to the existing document value, this means that the existing document will be replaced in the document store with the new one. Whilst patching operations are possible with RavenDB, the client API by default will always just replace the entire document in its entirety. /}
