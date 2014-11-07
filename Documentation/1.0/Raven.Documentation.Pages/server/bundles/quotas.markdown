﻿# Quotas Bundle

The Quotas Bundle is helpful when you want to restrict the size of a database. By setting a hard limit and a soft margin, the bundle will make sure you never exceed the space you designate for it to use.

The database size is calculated based on the Esent or Munin data files (excluding the logs), plus the space the indexes are taking on disk.

Once the hard limit has reached, no additional documents will be let in the document store. However, indexing operations _will_ resume normal operation even if that means the database size will go way over the hard limit. This is by design, to make sure the database is fully operational even when the hard limit has been met.

A soft limit is defined by the hard-limit minus the soft-limit margin, both are configurable. The first time the soft limit has been met, a warning will appear under `Raven/WarningMessages` with the `Size Quota` prefix.

## Installation

Put the Raven.Bundles.Quotas.dll file in the server's Plugins directory.

Configure the following values by adding entries to your app.config:

* **Raven/Quotas/Size/HardLimitInKB**  
    The hard limit in KBs.  
    _Default:_ no limit

* **Raven/Quotas/Size/SoftMarginInKB**  
    The margin value in KBs to start showing warning in the log.  
    _Default:_ 1024 ( = 1MB)