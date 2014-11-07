#HTTP API - Attachment Operations

## PUT

Perform a PUT request to /static/{attachment key} to create the specified attachment at the given URL:

{CODE-START:plain /}
    > curl -X PUT http://localhost:8080/static/users/ayende.jpg -d "[.. binary image data ...]"
{CODE-END /}

For a successful request, RavenDB will respond with the id it generated and an HTTP 201 Created response code:

{CODE-START:plain /}
    HTTP/1.1 201 Created
    Location: /static/users/ayende.jpg
{CODE-END /}

It is important to note that a PUT in RavenDB will always create the specified attachment at the request URL, if necessary overwriting what was there before.

While putting an attachment, it is possible to store metadata about it using HTTP Headers. The following standard HTTP headers will be stored and sent back when the attachment is next retrieved from Raven.

* Allow
* Content-Disposition
* Content-Encoding
* Content-Language
* Content-Location
* Content-MD5
* Content-Range
* Content-Type
* Expires
* Last-Modified

In addition to that, any custom HTTP header will also be stored and sent back to the client on GET requests for the attachment.

## GET
Raven supports the concept of attachments. Attachments are binary data that are stored in the database and can be retrieved by a key.
Retrieving an attachment is done by performing an HTTP GET on the following URL:

{CODE-START:plain /}
    > curl -X GET http://localhost:8080/static/{attachment key}
{CODE-END /}

For example, the following request:

{CODE-START:plain /}
    > curl -X GET http://localhost:8080/static/users/ayende.jpg
{CODE-END /}

Will retrieve an attachment whose key is "users/ayende.jpg", the response to the request is the exact byte stream that was stored in a previous [PUT](http://ravendb.net/docs/http-api/attachments/http-api-put-attachments) request.

## DELETE

Perform a DELETE request to delete the attachment specified by the URL:

{CODE-START:plain /}
    > curl -X DELETE http://localhost:8080/static/users/ayende.jpg
{CODE-END /}

For a successful delete, RavenDB will respond with an HTTP response code 204 No Content:

{CODE-START:plain /}
    "HTTP/1.1 204 No Content"
{CODE-END /}

The only way a delete can fail is if [the etag doesn't match](http://ravendb.net/docs/http-api/http-api-comcurrency?version=1.0), even if the attachment doesn't exist, a delete will still respond with a successful status code.