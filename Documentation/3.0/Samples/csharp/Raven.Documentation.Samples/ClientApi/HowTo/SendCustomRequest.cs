﻿using Raven.Abstractions.Data;
using Raven.Client.Connection;
using Raven.Client.Document;
using Raven.Json.Linq;

namespace Raven.Documentation.Samples.ClientApi.HowTo
{
	public class SendCustomRequest
	{
		public SendCustomRequest()
		{
			using (var store = new DocumentStore())
			{
				#region custom_request_1
				string key = "employees/1";

				// http://localhost:8080/databases/Northwind/docs/employees/1
				string url = store.Url // http://localhost:8080
					.ForDatabase("Northwind") // /databases/Northwind
					.Doc(key); // /docs/employees/1

				IDatabaseCommands commands = store.DatabaseCommands;
				HttpJsonRequest request = store
					.JsonRequestFactory
					.CreateHttpJsonRequest(new CreateHttpJsonRequestParams(commands, url, "GET", commands.PrimaryCredentials, store.Conventions));

				RavenJToken json = request.ReadResponseJson();
				JsonDocument jsonDocument = SerializationHelper.DeserializeJsonDocument(key, json, request.ResponseHeaders, request.ResponseStatusCode);
				#endregion
			}
		}
	}
}