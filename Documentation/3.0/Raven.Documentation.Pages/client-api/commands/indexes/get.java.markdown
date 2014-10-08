# Commands : Indexes : Get

There are few methods that allow you to retrieve index from a database:   
- [GetIndex](../../../client-api/commands/indexes/get#getindex)   
- [GetIndexes](../../../client-api/commands/indexes/get#getindexes)   
- [GetIndexNames](../../../client-api/commands/indexes/get#getindexnames)   

{PANEL:GetIndex}

**GetIndex** is used to retrieve an index definition from a database.

### Syntax

{CODE:java get_1_0@ClientApi\Commands\Indexes\Get.java /}

| Parameters | | |
| ------------- | ------------- | ----- |
| **name** | String | name of an index |

| Return Value | |
| ------------- | ----- |
| [IndexDefinition](../../../glossary/indexes/index-definition) | Instance of IndexDefinition repesenting index. |

### Example

{CODE:java get_1_1@ClientApi\Commands\Indexes\Get.java /}

{PANEL/}

{PANEL:GetIndexes}

**GetIndexes** is used to retrieve multiple index definitions from a database.

### Syntax

{CODE:java get_2_0@ClientApi\Commands\Indexes\Get.java /}

| Parameters | | |
| ------------- | ------------- | ----- |
| **start** | String | Number of documents that should be skipped |
| **pageSize** | int | Maximum number of documents that will be retrieved  |

| Return Value | |
| ------------- | ----- |
| Collection&lt;[IndexDefinition](../../../glossary/indexes/index-definition)&gt; | Collection of IndexDefinitions. |

### Example

{CODE:java get_2_1@ClientApi\Commands\Indexes\Get.java /}

{PANEL/}

{PANEL:GetIndexNames}

**GetIndexNames** is used to retrieve multiple index names from a database.

### Syntax

{CODE:java get_3_0@ClientApi\Commands\Indexes\Get.java /}

| Parameters | | |
| ------------- | ------------- | ----- |
| **start** | String | Number of documents that should be skipped |
| **pageSize** | int | Maximum number of documents that will be retrieved |

| Return Value | |
| ------------- | ----- |
| Collection&lt;String&gt; | This methods returns an array of index **name** as a result. |

### Example

{CODE:java get_3_1@ClientApi\Commands\Indexes\Get.java /}

{PANEL/}

## Related articles

- [PutIndex](../../../client-api/commands/indexes/put)  
- [DeleteIndex](../../../client-api/commands/indexes/delete)  