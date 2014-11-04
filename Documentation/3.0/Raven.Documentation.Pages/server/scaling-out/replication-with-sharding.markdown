# Mixing Replication and Sharding

Replication and Sharding are two powerful features supported by RavenDB. They are mostly orthogonal to one another, and you can use both replication and sharding together. It gets interesting with the deployment decisions that this leads to, which we will explore shortly. 

For the sake of simplicity, we will assume that only Users are stored in RavenDB, and that there is a simple sharding 2 node sharding scheme based on the user name (A - M in one node, N - Z in the second node).

## Sharding with dedicated failover nodes

In this setup, we have two nodes holding the users, and two nodes with serve as slaves for each master. If one of the primary nodes is failing, RavenDB will automatically switches to the replicated copy.

![Figure 1: Replication and Sharding](images\replication_and_sharding_docs.png)

{NOTE Should you allow the failover writes to the replicated node? If you do, you need to recover those writes. The usual method is to setup two way replication between the master and slave. Since the slave will be written to only if the master has failed, in essence what this means is that when the master restarts, we will be able to send it all the documents that were written while it was down. There is a small chance of getting conflicts while using this system, when a  write occurs on the server as soon as it is up, yet before replication from the slave has finished. /}

You need either to [handle that](../../server/scaling-out/replication/replication-conflicts), or choose to disallow writes while using the master is not possible, and then use the slave just for reads.

## Sharding with internal failover nodes

Another option is to use sharding primarily as a means of reducing load on the servers and  to setup replication between the different nodes without dedicated failover nodes, like this:

![Figure 2: Replication and Sharding](images\replication_and_sharding_docs_2.png)

{NOTE This reduces the number of instances that you use, but it also means that your capacity is limited to what can be stored in a single node. In practice, it means that if there is a failover, all the traffic will be directed to a single node. This may very well lead to a cascading failure scenario, since a single node may not be able to handle all the load from the application. /}

Even during normal operations, it puts each node under higher stress, since it needs to handle both normal operations and replicated operations.

## Related articles

TODO