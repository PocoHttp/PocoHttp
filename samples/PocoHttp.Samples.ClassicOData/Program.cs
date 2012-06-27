using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Northwinds;

namespace PocoHttp.Samples.ClassicOData
{
	class Program
	{
		static void Main(string[] args)
		{
			var testODataV1 = Test_OData_V1();
			Console.ForegroundColor = ConsoleColor.Yellow;
			testODataV1.ForEach(x=>Console.WriteLine(x));

			Console.WriteLine();

			var testODataV2 = Test_OData_V2();
			Console.ForegroundColor = ConsoleColor.Green;
			testODataV2.ForEach(x => Console.WriteLine(x));

			Console.Read();
		}


		private static List<Customer> Test_OData_V1()
		{
			var pocoClient = new PocoClient(new PocoConfiguration()
			{
				RequestSetup = (request) => request.Headers.Add("Accept", "application/json"),
				ResponseReader = (response, formatters, type) =>
				{
					//var innermostType = typeof(IEnumerable<>).MakeGenericType(type);
					var outermostType = typeof(PocoHttp.Samples.ClassicOData.V1.Payload<>).MakeGenericType(type);
					var taskCompletionSource = new TaskCompletionSource<object>();
					try
					{
						var result = response.Content.ReadAsAsync(outermostType, formatters).Result;
						var d = outermostType.GetProperty("d").GetValue(result, null);
						taskCompletionSource.SetResult(d);
					}
					catch (Exception e)
					{
						taskCompletionSource.SetException(e);
					}
					return taskCompletionSource.Task;
				}
			})
			{
				BaseAddress = new Uri("http://services.odata.org/Northwind/Northwind.svc/")
			};

			return pocoClient.Context<Customer>()
				.Skip(10).Take(2).ToList();

		}

		private static List<Customer> Test_OData_V2()
		{
			var pocoClient = new PocoClient(new PocoConfiguration()
			{
				RequestSetup = (request) => request.RequestUri = new Uri(request.RequestUri.ToString() + "&$format=json",UriKind.Relative),
				ResponseReader = (response, formatters, type) =>
				{
					//var innermostType = typeof(IEnumerable<>).MakeGenericType(type);
					var outermostType = typeof(PocoHttp.Samples.ClassicOData.V2.Payload<>).MakeGenericType(type);
					var taskCompletionSource = new TaskCompletionSource<object>();
					try
					{
						var result = response.Content.ReadAsAsync(outermostType, formatters).Result;
						var d = outermostType.GetProperty("d").GetValue(result, null);
						var results = d.GetType().GetProperty("results").GetValue(d, null);
						taskCompletionSource.SetResult(results);
					}
					catch (Exception e)
					{
						taskCompletionSource.SetException(e);
					}
					return taskCompletionSource.Task;
				}
			})
			{
				BaseAddress = new Uri("http://services.odata.org/Northwind/Northwind.svc/")
			};

			return pocoClient.Context<Customer>()
				.Skip(10).Take(25).ToList();

		}

	}
}
