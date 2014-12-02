# Dynamic fields

While strongly typed entities are well processed by LINQ expressions, some scenarios demand the use of dynamic properties. To support searching in object graphs they cannot have their entire structure declared upfront. RavenDB exposes low-level API for creating fields from within index definitions.

With this feature, you can search for documents even with properties which were created on the fly. For example, consider a `Product` object that is declared as follows:

{CODE dynamic_fields_1@Indexes\DynamicFields.cs /}

This way, properties such as color, size, weight, and the like are added only to those products of which they are indeed valid properties. However, while they are easily stored, they cannot be easily searched on.

This is where dynamic fields come in. With the following index definition, RavenDB will index the attribute value under the attribute name in its own field:

{CODE dynamic_fields_2@Indexes\DynamicFields.cs /}

The underscore used for defining the field name in the Map object is just a convention, you can use any field name instead, but since we just want to call the `CreateField` method and we are not interested in the field value, we are using `_` as a convention to reflect that.

The call to `CreateField(...)` will generate index fields based on the properties in the provided collection, without creating any field with the name specified there, hence the underscore.

Obviously, this index can have more attributes defined in it for indexing, just like any other ordinary index.

{INFO Field options like `FieldStorage.No` and `FieldIndexing.Analyzed` are configurable with dynamic fields as well. /}

When the index is created, we can easily look for documents using the attribute name as a field to look on, as if it was a real object property.

{CODE-TABS}
{CODE-TAB:csharp:Query dynamic_fields_4@Indexes\DynamicFields.cs /}
{CODE-TAB:csharp:DocumentQuery dynamic_fields_3@Indexes\DynamicFields.cs /}
{CODE-TABS/}

This will also work for numeric values, so range queries or searches with numeric operators like `WhereGreaterThan()` for `DocumentQuery` will work as well.

## Related articles

- [Boosting](../indexes/boosting)
- [Analyzers](../indexes/using-analyzers)
- [Storing data in index](../indexes/storing-data-in-index)
- [Term Vectors](../indexes/using-term-vectors)
