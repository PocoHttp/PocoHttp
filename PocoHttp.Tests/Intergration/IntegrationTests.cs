using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using NUnit.Framework;
using PocoHttp.Tests.Model;

namespace PocoHttp.Tests.Intergration
{
	[TestFixture]
	public class IntegrationTests
	{
		[Test]
		public void GetCars()
		{
			var pocoClient = new PocoClient()
				{BaseAddress = new Uri("http://localhost:56572/api/")};
			var list = pocoClient.Context<Car>()
				.Where(x=>x.Make == "Fiat").OrderBy(x=>x.Price).ToList();
			Console.WriteLine(list[1].Model);
		}

		[Test]
		public void GetCars2()
		{
			var request = new HttpRequestMessage(HttpMethod.Get, "/Cars");
			var httpClient = new HttpClient()
			                 	{
			                 		BaseAddress = new Uri("http://localhost:56572/api/")
			                 	};
			string result = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
			Console.WriteLine(result);
		}

	}
}
