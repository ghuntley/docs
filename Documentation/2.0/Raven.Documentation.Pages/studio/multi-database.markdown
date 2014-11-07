# Multi-Database Support

RavenDB supports multi-tenancy, and the Management Studio provides an easy way to manage multiple tenants from the same UI, and to create new tenants.

At the top right corner of each screen, by the RavenDB logo, we have several indicators:  
![](Images/studio_base_2.PNG)  

On the right we have a dot the represents the online status of the sever (red - offline, green online).  
On the left we have the name of the current tenant you are working against is shown. Clicking on it will open a list of other available tenants, and selecting another one from that list will shift the Studio to working against that tenant.  
In the middle we have the settings button which we will go into later in the page.

## Creating a new tenant
When you load the studio, if you don't have any databases on the server (other then the system database) you will get the "Create a new database" window.

When pressing on the link named "Databases" on the right of each page a new page will load:

![Databases Fig 1](Images/studio_databases_1.PNG)

On the top left you can create a new database:  
![Databases Fig 2](Images/studio_databases_2.PNG)

After selecting this option the "Create a new database" window will pop up:  
![Databases Fig 3](Images/studio_databases_3.PNG)

In this page you need to select a unique name for your database.  
On the right we can select which bundles you want to enable for your database (some bundles require a license to use).  
You can't change the selection of the Periodic Backup Bundle, it is selected according to your license.

Below the name you can check the advanced settings option which allows you to set the path for the database, logs and indexes:  
![Databases Fig 4](Images/studio_databases_4.PNG)  

Some of the bundles have settings needed to be set on database creation, if one or more of these bundles was selected after you press "Next" you will be promoted to those settings  
![Databases Fig 6](Images/studio_databases_6.PNG)  
For details of the bundles settings look in the [bundles section in the studio documentations](bundles)

After the database is created you will have the new database in the databases page, the new database will automatically be selected as the current database. (Note that the database my already contain documents depending on the bundles selected):  
![Databases Fig 7](Images/studio_databases_7.PNG)  

Once you have more the one database you can switch between them from any page by clicking on the name of the active database and a list of possible databases will open, click on the database you want to view next:  
![Databases Fig 5](Images/studio_databases_5.PNG)  

## Deleting a database
In order to delete a database you need to select it and then right-click on it and select "Delete" (you can also press the key "Delete"):  
![Databases Fig 8](Images/studio_databases_8.PNG)  

After that you will be promoted with the following window:  
![Databases Fig 9](Images/studio_databases_9.PNG)  
In here you can Export the database before deleting it.  
If you choose to physically delete all database data the folder(s) containing the database and all its related info (logs and indexes) will be deleted from your computer, otherwise the files will remain but you will not be able to access them from the server.

## Accessing the System Database
On the left of the database page you have the System Database button:  
![Databases Fig 10](Images/studio_databases_10.PNG)  
With this button you can access the system database (this is not advised!)  
After pressing the button you will get this conformation page:  
![Databases Fig 11](Images/studio_databases_11.PNG)  
After conformation you will be redirected to the "Documents" tab of the system database.  

## Database Settings
In order to get to the settings page of a database you have 2 options:  
1) Press the cog wheel icon next to the RavenDB logo in the top, from any page, in order to get to the settings of the selected database:  
![Databases Fig 12](Images/studio_databases_12.PNG)  
2) In the databases page, right-click on the database you wish to see the settings for and select "Edit settings":  
![Databases Fig 8](Images/studio_databases_8.PNG)  

Now you will arrive to the "Settings" page:  
![Databases Fig 13](Images/studio_databases_13.PNG) 

Some of the options are available for all databases, and some depend on the selected bundles.  

All databases will have the following:
1) Database Settings - In here you have see the DatabaseDocument that represents the database, you can edit this document, but this is not advised as you can cause damage to the database.
2) Periodic Backup - This section is explained in the [Periodic Backup Page](bundles/periodicbackup)

The Other options are dependent on the bundles selected for this database and are explained in the bundles section //TODO: Link

Pay attention that other then Database Settings, if you will not click on the "Save Changes" button in the top left any changes done in any bundle settings will be discarded.

## Database Settings for System Database  
The settings for the system database are different from the other databases.  
In order to get to the system databases settings you have to first select it from the Databases page and the click on the cog wheel button next to the RavenDB logo.  
![Databases Fig 14](Images/studio_databases_14.PNG)  
In here you have 3 sections.  
Periodic Backup is the same as it is for any database and is explained in the Bundles section //TODO: Link  

### Api Keys
In here you can set privileged access to databases.  
On the side you have the toolbar for the api keys settings:  
![Databases Fig 15](Images/studio_databases_15.PNG)  
New API Key - add a key for a user
Search - search keys by name

In order to add a key press the "New API Key" button:  
![Databases Fig 16](Images/studio_databases_16.PNG)  

In here you have several details you need to set:  

- Name - name of the user
- Secret - you cannot select it but you can generate it with the "Generate Secret" button next to it.
- Full Api Key, Connection Strings and Direct Link - those are generated from other parameters of the key with right-click you can copy those to the clipboard
- Enabled - check to enable this key.
- Databases - in here you set the access for each database
 - Database Name - name of the database (will auto complete)
 - Admin - Is user considered admin
 - Read Only - Is this database a read only for this user

With clicking on the + sign you can add another database to the list.  
Pressing of the X sight will remove this setting.  

A filled key will look like this:  
![Databases Fig 17](Images/studio_databases_17.PNG)  

### Windows Authentication
In Windows Authentication you can set user access for groups and users:  
![Databases Fig 18](Images/studio_databases_18.PNG)  
As you can see you have 2 tabs, one for Users and on for groups.  
In each tab you have the option to add another setting (for the respective list).  

Here is what you need to setup:  

- Name - the name of the user (as defined by windows) or of the group
- Secret - you cannot select it but you can generate it with the "Generate Secret" button next to it.
- Enabled - check to enable this settings.
- Databases - in here you set the access for each database
 - Database Name - name of the database (will auto complete)
 - Admin - Is user/group considered admin
 - Read Only - Is this database a read only for this user/group

With clicking on the + sign you can add another database to the list.  
Pressing of the X sight will remove this setting.  

A filled setting would look like this:  
![Databases Fig 19](Images/studio_databases_19.PNG)  