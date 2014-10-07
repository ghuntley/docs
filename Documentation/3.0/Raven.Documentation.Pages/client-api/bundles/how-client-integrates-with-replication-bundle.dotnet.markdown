# Replication : How client integrates with replication bundle?

RavenDB's Client API is aware of the replication mechanism offered by server instances and is ready to support failover scenarios.

{PANEL:**Failover behavior**}

 By default the client will detect and respond appropriately whenever a server has the replication bundle enabled. This includes:

* Detecting that an instance is replicating to another set of instances.
* When that instance is down, will automatically shift to the other instances.

This is because a failover mechanism is turned on in a document store by default. Then the client attempts to gain a replication document from `/docs/Raven/Replication/Destinations` in order to know what replication instances it would have use if a need of the failover occurred.

{NOTE Note that the client by default creates requests for the replication document even if the server has not the replication bundle enabled. Then you might notice in server's logs that requests for `/docs/Raven/Replication/Destinations` result in `404`./}

You are able to turn off the failover by using conventions of the document store. In order to do that use `FailImmediately` option:

{CODE client_integration_1@ClientApi\Bundles\HowClientIntegratesWithReplicationBundle.cs /}

The remaining values of `FailoverBehavior` enumeration are:

* *AllowReadsFromSecondaries* (default),
* *AllowReadsFromSecondariesAndWritesToSecondaries*,
* *ReadFromAllServers*.

They determine the strategy of the failovers if the primary server is down and the environment is configured to replicate between sibling instances.

{PANEL/}

{PANEL:**Discovering destinations**}

Once the document store is configured to support failovers it checks if the database server is configured to replicate. It retrieves a list of replicated nodes and saves it in a local application storage. Even if the primary server could no be reached in the future the list still exists locally and the document store can try to work with secondary instances according to conventions.

The Client API also checks if the replication configuration has changed on the server. It does it at regular intervals of 5 minutes to make sure that if the failover occurs, documents will go to instances that are slaves for our primary server.

{PANEL/}

{PANEL:**Failover servers**}

If primary server could not be reached by client and does not contain list of servers and such a list isn't available in local cache then client will try to load and use manually configured failover servers. List of those servers can be configured using `FailoverServers` property in `DocumentStore` or using .NET's named connection strings.

### Setup

{CODE client_integration_3@ClientApi\Bundles\HowClientIntegratesWithReplicationBundle.cs /}

### Setup using connection string

To setup failover using a [connection string](../../client-api/setting-up-connection-string) use `Failover` option. Multiple failovers can be setup using multiple `Failover` options.

Failover
:   Type: string in predefined format   
:   Format: JSON that can be deserialized to [ReplicationDestination](../../glossary/bundles/replication-destination) with optional database name separated with JSON using pipe ('|') e.g. `Northwind|{ ... }`      
Failover server definition.

Example:

{CODE client_integration_4@ClientApi\Bundles\HowClientIntegratesWithReplicationBundle.cs /}

Full example:

{CODE-BLOCK:plain}
    <connectionStrings>
        <add name="MyRavenConnectionStringName" connectionString="Url = http://localhost:59233;Failover = {Url:'http://localhost:8078'};Failover = {Url:'http://localhost:8077/', Database:'test'}; Failover = Northwind|{Url:'http://localhost:8076/'};Failover={Url:'http://localhost:8075', Username:'user', Password:'secret'};Failover={Url:'http://localhost:8074', ApiKey:'d5723e19-92ad-4531-adad-8611e6e05c8a'}" />
    </connectionStrings>
{CODE-BLOCK/}

{PANEL/}

{PANEL:**Setting up default client configuration on server**}

Default client configuration can be 'injected' into client, by filling out `ClientConfiguration` property in `Raven/Replication/Destinations`.

The available options are:

- `FailoverBehavior` - default failover behavior for all clients that are connecting to database.

Default configuration can be also be altered by using Studio. Appropriate settings are available in `Settings -> Replication`.

![Setting up default client configuration on server](images/replication-client-configuration.png)  

{PANEL/}

{PANEL:**Request redirection**}

The Raven Client API is quite intelligent in this regard, upon failure, it will:

* Assume that the failure is transient, and retry the request.
* If the second attempt fails as well, we record the failure and shift to a replicated node, if available.
* After ten consecutive failures, Raven will start replicating to this node less often
* * Once every 10 requests, until failure count reaches 100
* * Once every 100 requests, until failure count reaches 1,000
* * Once every 1,000 requests, when failure count is above 1,000
* On the first successful request, the failure count is reset.

If the second replicated node fails, the same logic applies to it as well, and we move to the third replicated node, and so on. If all nodes fail, an appropriate exception is thrown.

{PANEL/}

{PANEL:**Back to primary**}

The client that has been shifted to a replicated node will go back to its primary server 
as soon as the primary will become reachable (irrespective of the failure counter). In replication environment the nodes send heartbeat messages to notify destination instances that they are up again. Then the destination (which is the secondary server for our shifted client) will send a feedback message to client and then it tries to send request to the primary server again. If an operation succeeded the failure counter is reset and a communication starts to work normally.

{PANEL/}

{PANEL:**Replicated operations**}

At a lower level, those are the operations that support replication:

* Get - single document and multi documents
* Put
* Delete
* Query
* Rollback
* Commit

The following operation do not support replication in the Client API:

* PutIndex
* DeleteIndex

{PANEL/}

{PANEL:**Custom document ID generation**}

The usage of replication doesn't influence the algorithm of [a document ID generation](../../../client-api/basic-operations/saving-new-document#document-ids).
However in a Master/Master replication scenario it might be useful to add a server specific prefix to generated document identifiers. This would help to protect
against conflicts of document IDs between the replicating servers. In order to set up the server's prefix you have to put `Raven/ServerPrefixForHilo`:

{CODE client_integration_2@ClientApi\Bundles\HowClientIntegratesWithReplicationBundle.cs /}

The *ServerPrefix* value will be fetch in the same request as the current *HiLo* and will also become of a part of generated document IDs. 
For example storing a first `User` object will cause that its ID will be `Users/NorthServer/1`.

{PANEL/}