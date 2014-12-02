# Indexes
On the top right you have two options:  
![Indexes Fig 1](Images/studio_indexes_1.PNG)

When you choose to create a new index the following page will load:  
![Indexes Fig 2](Images/studio_indexes_2.PNG)  

When you choose Dynamic Query the following page will load:  
![Indexes Fig 3](Images/studio_indexes_3.PNG) 

In Recent Queries you can see and load again recent queries  
![Indexes Fig 15](Images/studio_indexes_15.PNG) 

With "Delete All Indexed" all indexes will be deleted except from "Raven/DocumentsByEntityName" (which is used by the studio)

In the body of the page you can see a list of the available indexes  
If you click on the index name a query page will load with ability to query the result of the index  
![Indexes Fig 4](Images/studio_indexes_4.PNG) 

If you click on the pencil icon of an index the edit index page will load  
![Indexes Fig 5](Images/studio_indexes_5.PNG)  

###Edit index or Create new index page <a id="create"></a>
In both pages the layout is the same  
On the top right you have several buttons  
![Indexes Fig 6](Images/studio_indexes_6.PNG)  
From right to left:  

- Save: will save the changes to the index 
 
- Add Map: adds map section to the index  
If you have more the one map you can delete extra maps with the X on the right![Indexes Fig 7](Images/studio_indexes_7.PNG) 

- Add reduce: adds reduce section to the index  
Only one reduce is possible, if you add a reduce the icon from the top will be removed, if the reduce section is deleted (by pressing the X on the right) it will return to the options  
![Indexes Fig 8](Images/studio_indexes_8.PNG) 

- Add transform: adds transform section to the index  
Only one transform is possible, if you add a transform the icon from the top will be removed, if the transform section is deleted (by pressing the X on the right) it will return to the options  
![Indexes Fig 9](Images/studio_indexes_9.PNG) 

- Add Field: adds a field section to the index   
It is possible to add as many fields as you want (you can remove a field by pressing the X)    
![Indexes Fig 10](Images/studio_indexes_10.PNG)

- Terms: Directs to the page where you can see the list of terms  
![Indexes Fig 16](Images/studio_indexes_16.PNG)

- Stats: Direct to the Index statistics  
![Indexes Fig 17](Images/studio_indexes_17.PNG)
- Query: Will send to the query page on the index 
- Undo: Will discard all changes made to the index
- Delete: Will delete the index

In the body of the page you have a place for the title of the index and a body for the map.  
Each index must have both a name and a map.  
![Indexes Fig 11](Images/studio_indexes_11.PNG)

###Query page <a id="query"></a>
In Dynamic query you on the top of the page you can choose if you want to query all the data or a specific collection  
![Indexes Fig 12](Images/studio_indexes_12.PNG)

Other then that the layout for dynamic query and index query is the same  
On the top right you have several buttons:  
![Indexes Fig 13](Images/studio_indexes_13.PNG)  

- Execute: Runs the query.
- Recent Queries: Shows and allows to go to recent queries
- Add Sort By: Add option to sort the results:  
![Indexes Fig 18](Images/studio_indexes_18.PNG)  

- Query Options: In here you can select if the default operator is OR/AND (default is OR), and if to show fields and index entries.  
![Indexes Fig 19](Images/studio_indexes_19.PNG)  


At the top of the body you have a space to type your query (with [lucene syntax](http://www.codeproject.com/Articles/29755/Introducing-Lucene-Net))  
At to bottom the results of the query  
![Indexes Fig 14](Images/studio_indexes_14.PNG) 

In the area for the query you can use Ctrl + Space for hints on field names:  
![Indexes Fig 20](Images/studio_indexes_20.PNG) 

