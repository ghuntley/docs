# Commands : Transformers : Put

**PutTransformer** is used to remove a transformer from a database.

## Syntax

{CODE put_1@ClientApi\Commands\Transformers\Put.cs /}

| Parameters | | |
| ------------- | ------------- | ----- |
| **name** | string | name of a transformer |
| **transformerDef** | [TransformerDefinition](../../../glossary/transformers/transformer-definition) | definition of a transformer |

| Return Value | |
| ------------- | ----- |
| string | Transformer **name**. |

## Example

{CODE put_2@ClientApi\Commands\Transformers\Put.cs /}

## Related articles

- [GetTransformer](../../../client-api/commands/transformers/get)  
- [DeleteTransformer](../../../client-api/commands/transformers/delete)  