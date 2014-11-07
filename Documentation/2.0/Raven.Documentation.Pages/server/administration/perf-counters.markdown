﻿#Performance Counters

RavenDB brings out information about its activity by using Windows built-in performance counters. By using [Windows Performance Monitor](http://technet.microsoft.com/en-us/library/cc749249.aspx) tool you are able to see a few
measurements exposed by the RavenDB server per each database. Under the category `RavenDB 2.0: [DATABASENAME]` you will see the following statistics related to a particular database instance:

* *# docs / sec* - the number of put documents per second,
* *# docs indexed / sec* - the number of indexed documents per second,
* *# docs reduced / sec* - the number of reduced documents per second,
* *# req / sec* - the number of requestes per second,
* *# of concurrent requests* - the number of concurrent requests.

##Setting up permissions for performance counters

RavenDB does not only manage own performance counters, but also takes advantage of the other ones exposed by Esent and .NET CLR in order to adjust behavior accordingly.
The access to the performance counters requires an administrator privileges, while it is a common scenario that RavenDB is not running by an admin (e.g. when deployed as IIS application). In this case to ensure RavenDB will be able to use 
the counters mechanism you have to setup the permissions for the user. You can accomplish it by using `Raven.Server.exe`:

{CODE-START:json /}
Raven.Server.exe /user=USERNAME /setup-perf-counters
{CODE-END /}

After executing this command, the specified user will be added to the Performance Monitoring Group what will give him the read/write privileges to the performance counters. 
Even if the user has been added successfully, the changes in the group membership are not effective unitl the next time the user logs on. So you will have to either login the given user again
or restart the IIS service it was IIS user.
 