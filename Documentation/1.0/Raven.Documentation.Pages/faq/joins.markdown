#Using joins in RavenDB

##How can I use joins in RavenDB?
The short answer is that you don't, RavenDB supports no joins, since it isn't a relational datastore.

The longer answer is that instead of joining, RavenDB (like other NoSQL solutions) provides alternative solutions.

* Denormalization (read more on [document based data modeling](http://ravendb.net/docs/theory/document-structure-design?version=1.0)) - This is the recommended approach

    Consider the case of music tracks and artists. In the relational world, you will have a column in the Tracks table that references the PK in the Artists table. To show a list of tracks with their artists, you will join between the two tables.
    In RavenDB, you will instead store in each Track document a denormalized reference to the artist, like so:

    {
          "Name" : "Fame",
          "Artist": { "Id": "artists/294", "Name": "Irene Cara" }
    }


    Yes, you are duplicating data. It's trade-off. But you are not duplicating whole Artist instance. Just the values you need to show in order to process requests pertaining to Track.

* **Client side joins**
    Of course, you can load the Artist by its id when you load a Track. This approach can be costly when you use it to load a collection of Tracks, so it is not recommended.
* **Server side joins**
    RavenDB's extensibility mechanism allows you to extend RavenDB to handle joins at the server side (eliminating network cost from simulating joins). The documentation about [Read Triggers](http://ravendb.net/docs/server/extending/triggers/read?version=1.0) contains an example of how to do just that.

When using denormalization, you'll probably want to look at how to make [denormalized updates](http://ravendb.net/docs/faq/denormalized-updates?version=1.0).
