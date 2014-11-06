﻿#Migrating attachments into RavenFS

In version 3.0 the usage of attachments is considered as obsolete. The recommended way to work with files is to use a distributed file system - RavenFS which is part of RavenDB. 
We provide a migration tool to support the migration of existing attachments into RavenFS.

{PANEL:**Creating a file system**}

In order to start migration process you need to have an existing file system where all attachments will be copied. Use [the management studio](../../studio/accessing-studio) to accomplish that.

After accessing a main page of the studio, switch to `File Systems`:

![Figure 1: Switch to file systems](images/migrate-attachments-create-fs-1.png)

Then create a new file system:

![Figure 2: Create a file system](images/migrate-attachments-create-fs-2.png)

{PANEL/}

{PANEL:**Using Raven.Migration utility**}

The utility `Raven.Migration.exe` is available in the `/Migration` folder of the ZIP package.

Usage example:

{CODE-BLOCK:plain}
Raven.Migration http://localhost:8080/ --database=MyDatabase --filesystem=MyFileSystem
{CODE-BLOCK/}

The above command will copy all attachments from `MyDatabase` database to the file system named `MyFileSystem` on the server `http://localhost:8080/`. If you want to migrate your attachments
to a file system on a different RavenDB server then you will have to additionally specify `fs-server` argument:

{CODE-BLOCK:plain}
Raven.Migration http://localhost:8080/ --database=MyDatabase --filesystem=MyFileSystem --fs-server=http://localhost:8081/
{CODE-BLOCK/}

The execution of such command will cause that all attachments from `MyDatabase` on `http://localhost:8080/` will be copied to `MyFileSystem` on `http://localhost:8081/` server.

The attachment migration utility will output progress info to the console window.

Available parameters:

* `database` - The database to copy attachments from.
* `filesystem` - The file system where attachments will be copied.
* `fs-server` - The URL of the RavenDB server where attachments will be copied to the specified file system.
* `db-username`, `db-password`, `db-domain` - The credentials to use if the database requires Windows authentication.
* `db-apikey` - The API-key to use if the database requires OAuth authentication.
* `fs-username`, `fs-password`, `fs-domain` - The credentials to use if the file system on requires Windows authentication.
* `fs-apikey` - The API-key to use if the database requires OAuth authentication.
* `delete-copied-attachments` - Delete an attachment after uploading it to the file system. Default: false.
* `batch-size` - Batch size for downloading attachments at once and uploading one-by-one to the file system. Default: 128.

{PANEL/}
