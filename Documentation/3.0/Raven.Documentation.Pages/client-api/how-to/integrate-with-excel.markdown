# How to integrate with Excel?

A very common use case for many application is to expose data to users as an Excel file. RavenDB has a dedicated support that allows to directly consume data stored in a database by Excel application. 
The integration of Excel with the data store is achieved by [query streaming](../session/querying/how-to-stream-query-results) and an appropriate output formatting understandable by Excel (Comma Separated Values).

In order to take advantage of this feature you need to specify [an index](../../indexes/what-are-indexes) that you want to query, a query itself and optionally [a result transformer](../../transformers/what-are-transformers) if you need to change the shape of results.
You have you also explicitly tell RavenDB to format results in Excel format.

The generic HTTP request will have the following address:

{CODE-BLOCK:plain}
http://localhost:8080/databases/[db_name]streams/query/[index_name]?query=[query]&resultsTransformer=[transformer_name]&format=excel
{CODE-BLOCK/}


## Example
 
Let's use the built-in Northwind sample data and default `Raven/DocumentsByEntityName` index. Let's also create a new transformer with the definition:

!['Products/ForExcel' transformer](images\excel_transformer.png)

In order to load into Excel all `Products` and transforming them according to `Products/ForExcel` transformer and we need to create following url:   

{CODE-BLOCK:plain}
http://localhost:8080/databases/Northwind/streams/query/Raven/DocumentsByEntityName?query=Tag:Products&resultsTransformer=Products/ForExcel&format=excel
{CODE-BLOCK/}

Going to the above address in a web browser will give you the following results:

![Query results](images\excel_result.png)

Now to push them to Excel we need to create new spreadsheet and import data `From Text`:

![Importing data from text in Excel](images\excel_from_text.png)

Then in a Open File Dialog we paste our querying url:

![Open File Dialog](images\excel_from_text_dialog.png)

Next, the Import Wizard will show up where we can adjust our import settings (don't forget to check `Comma` as a desired delimiter):

![Import Wizard Step 1](images\excel_from_text_wizard_1.png)

![Import Wizard Step 2](images\excel_from_text_wizard_2.png)

Finally we need to select where we would like to place the imported data:

![Select where to put the data](images\excel_from_text_select.png)

As a result of previous actions, the spreadsheet data should look like:

![Excel results](images\excel_from_text_results.png)

Now we must tell Excel to to refresh data. Click on `Connections` in `Data` panel:

![Excel connections](images\excel_connections.png)

You will see something like that:

![Excel connections dialog](images\excel_connections_dialog_1.png)

Go to Properties and:   
1. **uncheck** `Prompt for file name on refresh`.   
2. **check** `Refresh data when opening the file`.   

![Excel connection properties](images\excel_connections_dialog_2.png)

Finally you can close the file, change something in the database and reopen it. You will see new values.