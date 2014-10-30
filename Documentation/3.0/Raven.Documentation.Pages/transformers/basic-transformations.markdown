# Basic transformations

Assuming that you already know how to [create](../transformers/creating-and-deploying) transformers, you will want to know what can be done with them and what projection functions can be created.

{INFO:Projection function}
Transformers core is its projection function. It is a LINQ-based function with the ability to [load](../transformers/loading-documents) or [include](../transformers/including-documents) additional documents. [Parameters](../transformers/passing-parameters) can also be passed to customize the behavior.
{INFO/}

## Basics

To start, let's create a projection that will return only `FirstName` and `LastName` from each returned `Employee` from `Northwind` database.

- first, let's start creating a transformer `Employees/FirstAndLastName`

{CODE transformers_1@Transformers/Basics.cs /}

You probably noticed that we're passing `Employee` as a generic parameter to `AbstractTransformerCreationTask`. By doing this our transformation function will have a strongly-typed syntax.

- next step is to create a transformation itself and to do it we need to set the `TransformResults` property with our function in **parameterless constructor**.

{CODE-TABS}
{CODE-TAB:csharp:Query-syntax transformers_2@Transformers/Basics.cs /}
{CODE-TAB:csharp:Method-syntax transformers_3@Transformers/Basics.cs /}
{CODE-TABS/}

- final step is to [deploy it to the server](../transformers/creating-and-deploying) (omitted) and transform our query results using [TransformWith](../client-api/session/querying/how-to-use-transformers-in-queries) query extension method:

{CODE transformers_4@Transformers/Basics.cs /}

Probably `dynamic` is not the best return type so, obviously, projections to concrete types are supported:

{CODE transformers_5@Transformers/Basics.cs /}

{CODE transformers_6@Transformers/Basics.cs /}

Our final transformer looks like:

{CODE transformers_7@Transformers/Basics.cs /}

{WARNING:Important}
Before moving further, please note that property values of objects passed to projection function (in our example we are passing `employees`) are taken from stored index fields if present, otherwise they are loaded from a database.   

**Example** 

If we would [store](../indexes/storing-data-in-index) `FirstName` and `LastName` in index that was queried, then above transformer would use values from index directly, without loading them from database.  
{WARNING/}

## Projecting single property

You do not have to create new objects each time: when only single property is required, all you need to do is select that property:

{CODE transformers_8@Transformers/Basics.cs /}

{CODE transformers_9@Transformers/Basics.cs /}

## Projecting complex property

When your documents contain nested objects and you want to return only those, then projection can look as follows:

{CODE transformers_1_0@Transformers/Basics.cs /}

{CODE transformers_1_1@Transformers/Basics.cs /}

## Related articles

- [What are transformers?](../transformers/what-are-transformers)
- [Creating and deploying transformers](../transformers/creating-and-deploying)
- [Loading documents](../transformers/loading-documents)
