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
			
			// V1
			Console.ForegroundColor = ConsoleColor.Yellow;
			var testODataV1AndV2 = Test_OData_V1_And_V2(10, 2);
			testODataV1AndV2.ForEach(x => Console.WriteLine(x));
			Console.WriteLine();

			// V2
			Console.ForegroundColor = ConsoleColor.Yellow;
			testODataV1AndV2 = Test_OData_V1_And_V2(10, 25);
			testODataV1AndV2.ForEach(x => Console.WriteLine(x));

			Console.Read();
		}


		private static List<Customer> Test_OData_V1_And_V2(int skip, int count)
		{
			var pocoClient = new PocoClient(new PocoConfiguration()
			{
				RequestSetup = (request) => request.Headers.Add("Accept", "application/json"),
				ResponseReader = (response, formatters, type) =>
				{
					var versionHeader = response.Headers.FirstOrDefault(x=>x.Key == "DataServiceVersion");
					object result = null;
					if(versionHeader.Value.Any(x=> x!=null && x.Contains("1.0")))
					{
						result = ReadFromV1(response, formatters, type);
					}
					else if(versionHeader.Value.Any(x=> x!=null && x.Contains("2.0")))
					{
						result = ReadFromV2(response, formatters, type);						
					}
					else
					{
						throw new NotSupportedException("DataServiceVersion not supported.");
					}

					var taskCompletionSource = new TaskCompletionSource<object>();
					try
					{
						taskCompletionSource.SetResult(result);
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
				.Skip(skip).Take(count).ToList();
		}

		private static object ReadFromV1(HttpResponseMessage response, 
			IEnumerable<MediaTypeFormatter> formatters, Type type)
		{
			var outermostType = typeof(PocoHttp.Samples.ClassicOData.V1.Payload<>)
				.MakeGenericType(type);
			var result = response.Content.ReadAsAsync(outermostType, formatters).Result;
			return outermostType.GetProperty("d").GetValue(result, null);
		}

		private static object ReadFromV2(HttpResponseMessage response, 
			IEnumerable<MediaTypeFormatter> formatters, Type type)
		{
			var outermostType = typeof(PocoHttp.Samples.ClassicOData.V2.Payload<>)
				.MakeGenericType(type);
			var result = response.Content.ReadAsAsync(outermostType, formatters).Result;
			var d = outermostType.GetProperty("d").GetValue(result, null);
			return d.GetType().GetProperty("results").GetValue(d, null);
		}

	}
}
