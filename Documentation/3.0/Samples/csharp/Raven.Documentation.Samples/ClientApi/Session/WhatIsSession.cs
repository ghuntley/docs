﻿using System;

using Raven.Client;
using Raven.Client.Document;
using Raven.Documentation.CodeSamples.Orders;

using Xunit;

namespace Raven.Documentation.Samples.ClientApi.Session
{
	public class WhatIsSession
	{
		public WhatIsSession()
		{
			using (var store = new DocumentStore())
			{
				#region session_usage_1
				string companyId;
				using (IDocumentSession session = store.OpenSession())
				{
					Company entity = new Company { Name = "Company" };
					session.Store(entity);
					session.SaveChanges();
					companyId = entity.Id;
				}

				using (IDocumentSession session = store.OpenSession())
				{
					Company entity = session.Load<Company>(companyId);
					Console.WriteLine(entity.Name);
				}
				#endregion

				#region session_usage_2
				using (IDocumentSession session = store.OpenSession())
				{
					Company entity = session.Load<Company>(companyId);
					entity.Name = "Another Company";
					session.SaveChanges(); // will send the change to the database
				}
				#endregion

				using (var session = store.OpenSession())
				{
					#region session_usage_3
					Assert.Same(session.Load<Company>(companyId), session.Load<Company>(companyId));
					#endregion
				}
			}
		}
	}
}