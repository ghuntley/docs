﻿using System.Collections.Generic;
using System.Linq;

using Raven.Abstractions.Indexing;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Documentation.CodeSamples.Orders;

namespace Raven.Documentation.Samples.Indexes
{
	public class CustomizingResultsOrder
	{
		#region static_sorting1
		public class Products_ByUnitsInStock : AbstractIndexCreationTask<Product>
		{
			public Products_ByUnitsInStock()
			{
				Map = products => from product in products
								  select new
								  {
									  product.UnitsInStock
								  };

				Sort(x => x.UnitsInStock, SortOptions.Int);
			}
		}
		#endregion

		#region static_sorting2
		public class Products_ByName : AbstractIndexCreationTask<Product>
		{
			public Products_ByName()
			{
				Map = products => from product in products
								  select new
								  {
									  product.Name
								  };

				Sort(x => x.Name, SortOptions.String);

				Analyzers.Add(x => x.Name, "Raven.Database.Indexing.Collation.Cultures.SvCollationAnalyzer, Raven.Database");
			}
		}

		#endregion

		public void QueryWithOrderBy()
		{
			using (var store = new DocumentStore())
			using (var session = store.OpenSession())
			{
				#region static_sorting3
				IList<Product> results = session
					.Query<Product>()
					.OrderBy(product => product.UnitsInStock)
					.ToList();
				#endregion
			}


		}
	}
}