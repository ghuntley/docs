﻿# Recommended configuration

## Minimum requirements for RavenDB to run in embedded mode

* 512 MB of RAM
* Pentium III-class, 700MHz
* 150 MB of disk space
* 32 bit OS

Those are the _absolute minimum requirements_ for it to run. Please note that this is strictly for embedded mode, usually for dedicated devices such as Point of Sales systems where the amount of data and the number of queries are known in advance and the system has been configured specifically to allow that.

## Minimum requirements for RavenDB to run in server mode

* 2 GB of RAM
* 1 GHz Dual core
* 1 GB of disk space
* 64 bit OS

## Recommended configuration for RavenDB in server mode:

* 4 GB of RAM (more is better)
* 2 GHz Quad core (more is better)
* 50 GB of disk space (more is better)
* Note that splitting the db across multiple physical HDs is also recommended
* 64 bit OS

If you have any questions regarding deployment, please [contact us](http://ravendb.net/support).