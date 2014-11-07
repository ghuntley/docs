﻿using Raven.Abstractions.Data;
using Raven.Client.Connection;
using Raven.Client.Document;

namespace Raven.Documentation.Samples.ClientApi.Commands.Documents.HowTo
{
	public class DeleteOrUpdateByIndex
	{
		private interface IFoo
		{
			#region delete_by_index_1
			Operation DeleteByIndex(
				string indexName,
				IndexQuery queryToDelete,
				BulkOperationOptions options = null);
			#endregion

			#region update_by_index_1
			Operation UpdateByIndex(
				string indexName,
				IndexQuery queryToUpdate,
				PatchRequest[] patchRequests,
				BulkOperationOptions options = null);
			#endregion

			#region update_by_index_3
			Operation UpdateByIndex(
				string indexName,
				IndexQuery queryToUpdate,
				ScriptedPatchRequest patch,
				BulkOperationOptions options = null);
			#endregion
		}

		public DeleteOrUpdateByIndex()
		{
			using (var store = new DocumentStore())
			{
				#region delete_by_index_2
				// remove all documents from 'Employees' collection
				var operation = store
					.DatabaseCommands
					.DeleteByIndex(
						"Raven/DocumentsByEntityName",
						new IndexQuery
						{
							Query = "Tag:Employees"
						},
						new BulkOperationOptions
						{
							AllowStale = false
						});

				operation.WaitForCompletion();
				#endregion
			}

			using (var store = new DocumentStore())
			{
				#region update_by_index_2
				// Set property 'FirstName' for all documents in collection 'Employees' to 'Patched Name'
				var operation = store
					.DatabaseCommands
					.UpdateByIndex(
						"Raven/DocumentsByEntityName",
						new IndexQuery
						{
							Query = "Tag:Employees"
						},
						new[]
						{
							new PatchRequest
							{
								Type = PatchCommandType.Set, 
								Name = "FirstName", 
								Value = "Patched Name"
							}
						},
						new BulkOperationOptions
						{
							AllowStale = false
						});

				operation.WaitForCompletion();
				#endregion
			}

			using (var store = new DocumentStore())
			{
				#region update_by_index_4
				// Set property 'FirstName' for all documents in collection 'Employees' to 'Patched Name'
				var operation = store
					.DatabaseCommands
					.UpdateByIndex(
						"Raven/DocumentsByEntityName",
						new IndexQuery
						{
							Query = "Tag:Employees"
						},
						new ScriptedPatchRequest
						{
							Script = @"this.FirstName = 'Patched Name';"
						},
						new BulkOperationOptions
						{
							AllowStale = false
						});

				operation.WaitForCompletion();
				#endregion
			}
		}
	}
}