﻿# Opening a session

Once a document store has been created, the next step is to create a session against that document store that will allow us to perform basic CRUD operations within a Unit of Work. It is important to note that when invoking any operations against this store no changes will be made to the underlying document database until the `SaveChanges` method has been called:

{CODE open_the_session@ClientApi\BasicOperations\OpeningSession.cs /}

In this context, the session can be thought of as managing all changes internally, and `SaveChanges` can be thought of as committing all those changes to the RavenDB server. Any operations submitted in a ``SaveChanges`` call will be committed atomically (that is to say, either they all succeed, or they all fail).

It will be assumed in the following examples that a valid store has been created, and that the calls are being made within the context of a valid session, and that ``SaveChanges`` is being called safely at the end of that session lifetime.

{WARNING If you don't call `SaveChanges`, all the changes made in that session will be discarded! /}

Whenever database access is needed, we are going to open a new session, and dispose of it right after we are done using it (and called `SaveChanges`).