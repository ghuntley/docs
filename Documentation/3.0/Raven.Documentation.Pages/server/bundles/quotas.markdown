# Bundle : Quotas

The Quotas Bundle is helpful when you want to restrict the size of a database. By setting a hard limit and a soft margin, the bundle will make sure you never exceed the space you designate for it to use.

The database size is calculated based on the Esent or Voron data files (excluding the logs), plus the space the indexes are taking on disk.

Once the hard limit has reached, no additional documents will be let in the document store. However, indexing operations _will_ resume normal operation even if that means the database size will go way over the hard limit. This is by design, to make sure the database is fully operational even when the hard limit has been met.

A soft limit is defined by the hard-limit minus the soft-limit margin, both are configurable. The first time the soft limit has been met, a warning will appear under `Raven/WarningMessages` with the `Size Quota` prefix.

## Installation

To activate compression server-wide just add `Quotas` to `Raven/ActiveBundles` configuration in global configuration file or setup new database with compression bundle turned on using API or Studio.

How to create a database with quotas enabled using Studio can be found [here](../../../studio/bundles/quotas).

## Configuration

Configure the following values by adding entries to your [server configuration](../../administration/configuration) or [database settings](../../multiple-databases):

* **Raven/Quotas/Size/HardLimitInKB**
	The hard limit after which we refuse any additional writes.   
	_Default:_ none

* **Raven/Quotas/Size/SoftMarginInKB**
	The soft limit before which we will warn about the quota.   
	_Default:_ 1024

* **Raven/Quotas/Documents/HardLimit**
	The hard limit after which we refuse any additional documents.   
	_Default:_ Int64.MaxValue

* **Raven/Quotas/Documents/SoftLimit**
	The soft limit before which we will warn about the document limit quota.   
	_Default:_ Int64.MaxValue

## Related articles

TODO