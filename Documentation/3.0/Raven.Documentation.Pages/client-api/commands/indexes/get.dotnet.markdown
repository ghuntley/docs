# Commands : Indexes : Get

There are few methods that allow you to retrieve index from a database:   
- [GetIndex](../../../client-api/commands/indexes/get#getindex)   
- [GetIndexes](../../../client-api/commands/indexes/get#getindexes)   
- [GetIndexNames](../../../client-api/commands/indexes/get#getindexnames)   

{PANEL:GetIndex}

**GetIndex** is used to retrieve an index definition from a database.

### Syntax

{CODE get_1_0@ClientApi\Commands\Indexes\Get.cs /}

| Parameters | | |
| ------------- | ------------- | ----- |
| **name** | string | name of an index |

| Return Value | |
| ------------- | ----- |
| [IndexDefinition](../../../glossary/indexes/index-definition) | Instance of IndexDefinition repesenting index. |

### Example

{CODE get_1_1@ClientApi\Commands\Indexes\Get.cs /}

{PANEL/}

{PANEL:GetIndexes}

**GetIndexes** is used to retrieve multiple index definitions from a database.

### Syntax

{CODE get_2_0@ClientApi\Commands\Indexes\Get.cs /}

| Parameters | | |
| ------------- | ------------- | ----- |
| **start** | string | Number of documents that should be skipped |
| **pageSize** | int | Maximum number of documents that will be retrieved  |

| Return Value | |
| ------------- | ----- |
| [IndexDefinition](../../../glossary/indexes/index-definition) | Instance of IndexDefinition repesenting index. |

### Example

{CODE get_2_1@ClientApi\Commands\Indexes\Get.cs /}

{PANEL/}

{PANEL:GetIndexNames}

**GetIndexNames** is used to retrieve multiple index names from a database.

### Syntax

{CODE get_3_0@ClientApi\Commands\Indexes\Get.cs /}

| Parameters | | |
| ------------- | ------------- | ----- |
| **start** | string | Number of documents that should be skipped |
| **pageSize** | int | Maximum number of documents that will be retrieved |

| Return Value | |
| ------------- | ----- |
| string[] | This methods returns an array of index **name** as a result. |

### Example

{CODE get_3_1@ClientApi\Commands\Indexes\Get.cs /}

{PANEL/}

## Related articles

- [PutIndex](../../../client-api/commands/indexes/put)  
- [DeleteIndex](../../../client-api/commands/indexes/delete)  