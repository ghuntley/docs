﻿using Raven.Client.Document;
using Raven.Documentation.CodeSamples.Orders;

namespace Raven.Documentation.Samples.ClientApi.Session.HowTo
{
	public class HasChanged
	{
		private interface IFoo
		{
			#region has_changed_1
			bool HasChanged(object entity);
			#endregion
		}

		public HasChanged()
		{
			using (var store = new DocumentStore())
			{
				using (var session = store.OpenSession())
				{
					#region has_changed_2
					Employee employee = session.Load<Employee>("employees/1");
					bool hasChanged = session.Advanced.HasChanged(employee); // false
					employee.LastName = "Shmoe";
					hasChanged = session.Advanced.HasChanged(employee); // true
					#endregion
				}
			}
		}
	}
}