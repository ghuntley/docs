# Dynamic fields

While strongly typed entities are well processed by Linq expressions, some scenarios demand the use of dynamic properties. To support searching in object graphs that cannot have their entire structure be declared upfront, RavenDB exposes the following low-level API for creating fields from within index definitions.

With this feature, you can search for documents even with properties which were created on the fly. For example, consider a `Product` object that is declared as follows:

{CODE-START:csharp /}
		public class Product
		{
			public string Id { get; set; }
			public List<Attribute> Attributes { get; set; }
		}

		public class Attribute
		{
			public string Name { get; set; }
			public string Value { get; set; }
		}
{CODE-END /}

This way, properties such as color, size, weight and the like are added only to those products which they are indeed valid properties of. However, while they are easily stored, they cannot be easily searched on.

This is where dynamic fields come in. With the following index definition, RavenDB will index the attribute value under the attribute name in its own field:

{CODE-START:csharp /}
		public class Product_ByAttribute : AbstractIndexCreationTask<Product>
		{
			public Product_ByAttribute()
			{
				Map = products =>
					from p in products
					select new
					{
						_ = p.Attributes.Select(attribute =>
              CreateField(attribute.Name, attribute.Value, stored: false, analyzed: true))
					};
			}
		}
{CODE-END /}

The underscore used for defining the field name in the Map object is just a convention, you can use any field name instead, but since that we're just want to call the `CreateField` method and not interesting the field value, we're using `_` as a convention to reflect that.

The call to `CreateField(...)` will generate index fields based on the properties in the provided collection, without creating any field with the name specified there, hence the underscore.

Obviously, this index can have more attributes defined in it for indexing, just like any other ordinary index.

{INFO Field options like Store.NO and Index.ANALYZED are configurable also with dynamic fields. /}

After creating the index, we can easily look for documents using the attribute name as a field to look on, as if it was a real object property. Since it is not really a property, there is no Linq support for it, hence it can only be queried using the `LuceneQuery<>()` API:

{CODE-START:csharp /}
					var products = session.Advanced.LuceneQuery<Product>("Product/ByAttribute")
						.WhereEquals("Color", "Red")
						.ToList();
{CODE-END /}

This will also work for numeric values, so range queries or searches with numeric operators like `WhereGreaterThan()` will work as well.
