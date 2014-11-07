﻿using Raven.Client.Document;
using Raven.Documentation.CodeSamples.Orders;

namespace Raven.Documentation.Samples.ClientApi.Session.Configuration
{
	public class OptimisticConcurrency
	{
		public OptimisticConcurrency()
		{
			using (var store = new DocumentStore())
			{
				#region optimistic_concurrency_1
				using (var session = store.OpenSession())
				{
					session.Advanced.UseOptimisticConcurrency = true;

					var product = new Product { Name = "Some Name" };

					session.Store(product, "products/999");
					session.SaveChanges();

					using (var otherSession = store.OpenSession())
					{
						var otherProduct = otherSession.Load<Product>("products/999");
						otherProduct.Name = "Other Name";

						otherSession.SaveChanges();
					}

					product.Name = "Better Name";
					session.SaveChanges(); // will throw ConcurrencyException
				}
				#endregion

				#region optimistic_concurrency_2
				store.Conventions.DefaultUseOptimisticConcurrency = true;

				using (var session = store.OpenSession())
				{
					var isSessionUsingOptimisticConcurrency = session.Advanced.UseOptimisticConcurrency; // will return true
				}
				#endregion
			}
		}
	}
}