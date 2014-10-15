# Changes API : How to subscribe to bulk insert operation changes?

`forBulkInsert` method can be used to aquire notifications for bulk insert operations.

## Syntax

{CODE:java bulk_insert_changes_1@ClientApi\Changes\HowToSubscribeToBulkInsertChanges.java /}

| Parameters | | |
| ------------- | ------------- | ----- |
| **operationId** | UUID | Id of bulk insert operation, generated when bulk insert operation is created. |

| ReturnValue | |
| ------------- | ----- |
| IObservable<[BulkInsertChangeNotification](../../glossary/client-api/changes/bulk-insert-change-notification)> | Observable that allows to add subscribtions to notifications for bulk insert operation with given id. |

## Example

{CODE:java bulk_insert_changes_2@ClientApi\Changes\HowToSubscribeToBulkInsertChanges.java /}

## Related articles

TODO