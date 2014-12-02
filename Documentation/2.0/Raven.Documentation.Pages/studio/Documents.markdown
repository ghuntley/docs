# The Documents Screen

This screen gives an overview of all documents in the RavenDB server instance.

## Documents listing

The documents are loaded from the database, newest first, documents from different collections are striped with different colors, in the default view you will get the "Details" view :

![Figure 1: Documents listing](Images/studio_documents_1.PNG)

{INFO A **Collection** in RavenDB is a group of documents sharing the same entity name. It is not a "database table", but rather a logical way of thinking of document groups. /}

In the left of the view you will find a couple of buttons.

![](Images/studio_base_11.PNG)

The left button allows you to Export to CSV.

The right button allows you to change the size of the document preview in the listing - from Details view to Extra Large Card view that will show the data of the document as a JSON string (by clicking on the icon toggles between the different views):

![Figure 2: Changing the size of the document preview in listing](Images/studio_base_7.PNG)  
The "Id Only" option is recommended when using remote server as it uses less bandwidth.

## Editing a document
By double clicking on a document in the list, or clicking on the pencil icon on it, you get to the document editing screen.

The document ID

 - a unique key assigned to each document - is shown at the top of the form, and below that the document in its JSON representation is ready to be edited:

![Figure 4: Editing a document](Images/studio_documents_4.PNG)

RavenDB saves additional data on each document, such as it's Collection association (`Raven-Entity-Name`), and all that info can be viewed and edited through the metadata tab:

![Figure 5: Editing metadata](Images/studio_documents_7.PNG)

On the top right corner of this screen you will find several buttons:

- Save changes
- Reformat Document: will remove empty line and fix indentation
- Outlining: Select whether to enable, disable or auto collapse outlining
- Refresh: will reload the document from the server
- Delete Document: to permanently delete the current document, will prompt before doing the delete
- Search
- Paging between the neighboring documents

![](Images/studio_documents_9.PNG)

To the right of the form some statistics are shown - the metadata of the current document, formatted, and a list of possible reference found in it. Clicking on a reference will open that document for editing, if it exists in your database.

![Document Fig 8](Images/studio_documents_8.PNG)

## Other options

Back in the main listing screen, at the top right corner of the screen you will find 2 additional Options:  

![](Images/studio_base_5.PNG)

Click on the "Go To Document" and start typing the id of the document you want to edit and suggestions for your in will appear:

![](Images/studio_base_9.PNG)

If no document exists in the database for the ID you provide you will be redirected to the Documents tab, otherwise the Document edit page will load with the requested document.

Clicking the "New" button will open the document editing screen, but this time with empty fields. After providing a unique ID for the document and filling some data for it, clicking on Save will create it on the server.

If you click on the arrow by the "New" button you will have several options where you will go to the respective pages for each item you want to create:

![](Images/studio_base_10.PNG)

For a new document created through the Studio the `Raven-entity-Name` metadata attribute which sets its collection is set by the text before the '/' in the provided ID (with first letter in capital), so the a document with ID "albums/626" will be assigned to the "Albums" collection.

If you have a a multi-tenant database set up, while browsing the documents in the Default Database tenant you will see one or more special documents titled "Sys Doc" with the names of the available tenants. Do not edit or delete this document - doing so might cause you to lose access to that tenant, although the actual data in it is not deleted.

![Document Fig 2](Images/studio_documents_2.PNG)
