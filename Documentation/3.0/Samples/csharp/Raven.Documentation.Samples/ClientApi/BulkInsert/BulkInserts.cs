﻿using Raven.Abstractions.Data;
using Raven.Client.Document;
using Raven.Documentation.CodeSamples.Orders;

namespace Raven.Documentation.Samples.ClientApi.BulkInsert
{
	public class BulkInserts
	{
		private interface IFoo
		{
			#region bulk_inserts_1
			BulkInsertOperation BulkInsert(
				string database = null,
				BulkInsertOptions options = null);
			#endregion
		}

		public BulkInserts()
		{
			using (var store = new DocumentStore())
			{
				#region bulk_inserts_4
				using (BulkInsertOperation bulkInsert = store.BulkInsert())
				{
					for (int i = 0; i < 1000 * 1000; i++)
					{
						bulkInsert.Store(new Employee
						{
							FirstName = "FirstName #" + i,
							LastName = "LastName #" + i
						});
					}
				}
				#endregion
			}
		}
	}
}