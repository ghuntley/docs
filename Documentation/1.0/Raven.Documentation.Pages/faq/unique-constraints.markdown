#Creating unique constraints
This is a common question:

    How can I create a unique constraint in RavenDB on a property that isn't the id?

And the answer is quite simple, you don't. RavenDB doesn't provide any facility to enforce a unique constraint, nor can you use a standard index to try to enforce uniqueness. The reason for this limitation is that indexes are built on the background, and it is entirely possible that a document that was inserted by not yet indexed contains the value that you are trying to ensure does not exists.

Instead, the way to handle unique constraints in RavenDB is to utilize the one thing that it does ensure will be transactionally unique: the id. That requires us to maintain a separate sets of documents for the unique properties that we want to ensure.

We will use the username & email example, where both the username and the email address must be unique. Making sure that the username is unique is easy, we just make the username the document key, so we have documents with the following key:

* users/ayende
* users/oren

Now, we also need to verify that the user's email is unique. In order to do that, we create a separate set of documents, keyed by the user's email:

* emails/ayende@ayende.com - { "UserId": "users/ayende" }
* emails/oren@oreneini.com - { "UserId": "users/oren" }

Each email document is keyed by the email, and the only content it has is a reference to the appropriate user.

Since RavenDB ensures total uniqueness of the values, and since you can store multiple documents in a single batch, you get transactional guarantees for that.

Creating a new user them becomes an issue of:

    s.Advanced.UseOptimisticConcurrency = true;
    s.Store(new User { Id = "users/ayende", Name = "Ayende Rahien", Email = "ayende@ayende.com" });
    s.Store(new EmailREference { Id = "emails/ayende@ayende.com", UserId = "users/ayende" });
    s.SaveChanges(); // if there already exists a document with either name, it would fail the transaction

Using this approach, we enforce uniqueness by email, and we can extend this to however many unique properties we want.