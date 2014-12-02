# Bundle: Periodic Backup

In order to enable `Periodic Backup` you need to enter database settings by pressing `Settings` on a top navigation panel.   
![Periodic Backup Fig 1](Images/studio_periodic_1.PNG)  

Once there select the "Periodic Backup" option:  
![Periodic Backup Fig 2](Images/studio_periodic_2.PNG)  

By default backup is not configured and disabled. To activate it press **Activate Periodic Backup**, this will activate the backup settings where you will be able to choose your backup type and configure it properly.
![Periodic Backup Fig 3](Images/studio_periodic_3.PNG)  

Available backup types:

![Periodic Backup Fig 4](Images/studio_periodic_4.PNG)  

- **File System Folder** where you will need to specify:
 - backup path   
 - interval  

![Periodic Backup Fig 5](Images/studio_periodic_5.PNG)  
 
- **Glacier Vault** or **S3 Bucket** with following configuration options:
 - name of the vault or bucket
 - AWS Access and Secret keys
 - AWS Region
 - interval  

![Periodic Backup Fig 6](Images/studio_periodic_6.PNG)  
 
- **Azure Storage** with following fields to fill:
 - name of the storage   
 - Azure Storage Account and Key
 - interval

![Periodic Backup Fig 7](Images/studio_periodic_7.PNG)  
