# Indexes

On the top right you have two options:  
![Indexes Fig 1](Images/studio_indexes_1.PNG)

When you choose to create a new index the following page will load:  
![Indexes Fig 2](Images/studio_indexes_2.PNG)  

When you choose Dynamic Query the following page will load:  
![Indexes Fig 3](Images/studio_indexes_3.PNG) 

In Recent Queries you can see and load again recent queries  
![Indexes Fig 15](Images/studio_indexes_15.PNG) 

With "Delete All Idle Indexes" all idle indexes will be deleted. There are also other options available:

![Indexes Fig 15_1](Images/studio_indexes_15_1.PNG) 

{NOTE `Raven/DocumentsByEntityName` index will never be deleted (it is used by the Studio). /}

In the body of the page you can see a list of the available indexes  
If you click on the index name a query page will load with ability to query the result of the index  
![Indexes Fig 4](Images/studio_indexes_4.PNG) 

If you click on the pencil icon of an index the edit index page will load  
![Indexes Fig 5](Images/studio_indexes_5.PNG)  

## Creating new and editing index

In both pages the layout is the same  
On the top right you have several buttons  

![Indexes Fig 6](Images/studio_indexes_6.PNG)  

From right to left:  
- **Save**: will save the changes to the index   
    
- **Add Map**: adds map section to the index  
If you have more the one map you can delete extra maps with the X on the right   
![Indexes Fig 7](Images/studio_indexes_7.PNG)   
   
- **Add Reduce**: adds reduce section to the index  
Only one reduce is possible, if you add a reduce the icon from the top will be removed, if the reduce section is deleted (by pressing the X on the right) it will return to the options  
![Indexes Fig 8](Images/studio_indexes_8.PNG)    
   
- **Add Field**: adds a field section to the index   
It is possible to add as many fields as you want (you can remove a field by pressing the X)    
![Indexes Fig 10](Images/studio_indexes_10.PNG)   
   
- **Add Transform**: adds transform section to the index  
Only one transform is possible, if you add a transform the icon from the top will be removed, if the transform section is deleted (by pressing the X on the right) it will return to the options  
![Indexes Fig 9](Images/studio_indexes_9.PNG)   
   
- **Add Spatial Field**: adds a spatial field section to the index   
It is possible to add as many apatial fields as you want (you can remove a field by pressing the X)    
![Indexes Fig 10](Images/studio_indexes_10_1.PNG)    
   
- **Index Priority**: you can choose what priority will current index have.         
- **Terms**: Directs to the page where you can see the list of terms  
![Indexes Fig 16](Images/studio_indexes_16.PNG)    
   
- **Stats**: Directs to the Index statistics  
![Indexes Fig 17](Images/studio_indexes_17.PNG)    
- **Query**: Will send to the query page on the index 
- **Undo**: Will discard all changes made to the index
- **Delete**: Will delete the index
   
In the body of the page you have a place for the title of the index and a body for the map.  
Each index must have both a name and a map.  
![Indexes Fig 11](Images/studio_indexes_11.PNG)

## Querying index

In `Dynamic Query`, at the top of the page, user can choose a collection (or all documents) for which the query will be executed.   

![Indexes Fig 12](Images/studio_indexes_12.PNG)

Other then that the layout for dynamic query and index query is the same  
At the top right you have several buttons:  
![Indexes Fig 13](Images/studio_indexes_13.PNG)  

- **Execute**: Runs the query.
- **Recent Queries**: Shows and allows to go to recent queries
- **Add Sort By**: Add option to sort the results:  
![Indexes Fig 18](Images/studio_indexes_18.PNG)  

- **Add Transformer**: Adds a possibility to use a [Transformer](../client-api/querying/results-transformation/result-transformers) on the results:  
![Indexes Fig 18](Images/studio_indexes_18_1.PNG)  

- **Query Options**: In here you can select if the default operator is OR/AND (default is OR), and if to show fields and index entries.  
![Indexes Fig 19](Images/studio_indexes_19.PNG)  

At the top of the body you have a space to type your query (with [lucene syntax](http://www.codeproject.com/Articles/29755/Introducing-Lucene-Net))  
At to bottom the results of the query  
![Indexes Fig 14](Images/studio_indexes_14.PNG) 

In the area for the query you can use Ctrl + Space for hints on field names:  
![Indexes Fig 20](Images/studio_indexes_20.PNG) 

