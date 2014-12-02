#Advanced replication details

An activation of the replication bundle will have an impact on how your database will act. There are some internal procedures that are executed only if this module is enabled. Note that is it enough the bundle is active, it doesn't have to be configured to indicate any destination node. Below there are listed implications of active replication that you should be aware.

##Activating bundle

The replication bundle provides a mechanism to detect that a document was modified and there is a need to propagate it to a destination, but it also handles the replication on a destination database. So you have to remember about an activation of the replication bundle in both databases.

##Influence on metadata

The replication module arrives with triggers performed on every PUT operation. They take care of tracking an ancestry of a document (or an attachment) and building its history when it moves between database instances. These information are stored in metadata under special system keys:

* *Raven-Replication-Version* that describes a file version in a database,
* *Raven-Replication-Source* which is a database identifier where a file was put,
* *Raven-Replication-History* where old pairs of a source and a version are collected.

As it was mentioned these values are added to metadata only if the replication is active. It means that the scenario when you want to enable database replication when documents are already there is not supported.

##History

A destination RavenDB instance uses history of a replicated document to detect if there is a conflict. The more edits of the document is made the longest history it has. RavenDB tracks history of last 50 document's modification. After exceeding this value history is trimmed. Notice that might cause document conflicts even if Master/Slave configuration was applied. Each edit of a document pushes a last history item out of a history list. When it happens more that 50 modifications to the same document between two replication cycles, history will contain only information about new changes that the destination database has never seen. The destination will not be able to detects that its document is an ancestor of a replicated document and will make the conflict.

##Tombstones

In RavenDB there is a concept of tombstone that represent a deleted document. Every document delete operation entails to store a tombstone document. This mechanism is used by the replication and it works only if the replication bundle is active. The stored tombstone documents are used to reflect a delete document operation on a destination database. Although the tombstones are created in a different storage area than the documents they are sill included in database statistics, that's why the document count does not decrease after a delete if the replication is enabled.
