# Configuration : Storage Engines

{PANEL}

RavenDB supports two storage engines and each database can be created with a different one:

- _(Default)_ ESE (Extensible Storage Engine aka Esent) - Microsoft storage engine available in Windows. More info [here](http://en.wikipedia.org/wiki/Extensible_Storage_Engine).
- Voron - custom made managed storage engine created by Hibernating Rhinos.

{PANEL/}

{PANEL:**Voron**}

### Limitations

- no support for DTC transactions
- no support for [compaction](../../client-api/commands/how-to/compact-database)

### Requirements

- disk must handle UNBUFFERED_IO/WRITE_THROUGH properly
- .NET 4.5 or higher
- [Hotfix](http://support.microsoft.com/kb/2731284) for Windows 7 and Windows Server 2008 R2

{INFO You can watch [Level 400 - Diving into Voron](https://www.youtube.com/watch?v=yJqOEqqUfUA) talk by Oren Eini if you are interested in details about Voron. /}

{PANEL/}

{PANEL:**Using Studio**}

In order to change a storage engine in the Studio, while you are creating a database, go to `Advanced Settings` and change `Storage Engine` according to your needs.

![Figure 1: Creating database and changing Storage Engine](images/create-database-select-engine-studio.png)

{PANEL/}