# Bundle: Periodic Backup

RavenDB comes with support of doing periodic backup of documents and attachments to [Amazon AWS](http://aws.amazon.com/) services.  
When creating a database in the studio you will see that you can't change the selection of the 'Periodic Backup Bundle' it's selection depends on the license.  
In order to periodic backup to work you must activate `PeriodicBackup` bundle, by activating this bundle globally and turning it on/off per database, or activating it per database only.

## How it works

Periodic backup are leveraging the concept of incremental backups available in RavenDB and to take advantage of that, we are storing an information about last successful ETag of the documents and attachments that were send to backup destination.

## Configuration

### Activating bundle

To activate bundle globally just add `PeriodicBackup` to the `Raven/ActiveBundles`. More about setting up configuration can be found [here](../Administration/configuration).

If you wish to setup periodic backup per database, then add `PeriodicBackup` to the list of database active bundles or use [the Studio](../../studio/bundles/periodicbackup).

Bundle can also be activated during database creation process.

{CODE periodic_backups_1@Server\Extending\Bundles\PeriodicBackup.cs /}

### Configuring backup destination

Two steps need to be taken to setup backup destination properly.

First we need to add our AWS access and secret key to database settings. For example if we want to create new database with bundle already activated and keys setup, then we can execute following code:

{CODE periodic_backups_2@Server\Extending\Bundles\PeriodicBackup.cs /}

In next step we need to create a backup setup document under `Raven/Backup/Periodic/Setup` where we will store our backup destination configuration. This document will be created automatically when you will use Studio to setup periodic backup, but it can be created almost as easily using the API.

{CODE periodic_backups_3@Server\Extending\Bundles\PeriodicBackup.cs /}

`GlacierVaultName` and `S3BucketName` values **exclude** each other in favor of the `GlacierVaultName` so if you will specify both, then RavenDB will only use `GlacierVaultName`. 

{NOTE More information about Amazon Simple Storage Service (Amazon S3) can be found [here](http://aws.amazon.com/s3/) and if you are interested in Amazon Glacier then visit [this](http://aws.amazon.com/glacier/) page. /}

