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
			var pocoClient = new PocoClient(new PocoConfiguration()
			    {
			        RequestSetup = (request) => request.Headers.Add("Accept", "application/json"),
			        ResponseReader = (response, formatters, type) =>
			                            {
			                                //var innermostType = typeof(IEnumerable<>).MakeGenericType(type);
											var outermostType = typeof(Payload<>).MakeGenericType(type);
			                                var taskCompletionSource = new TaskCompletionSource<object>();
			                                try
			                                {
			                                	var result = response.Content.ReadAsAsync(outermostType, formatters).Result;
			                                	var payload = (Payload<object>) result;
												taskCompletionSource.SetResult(payload.d.results);
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

			var list = pocoClient.Context<Customer>()
				.Skip(10).Take(2).ToList();

			Console.Read();
		}
	}
}
