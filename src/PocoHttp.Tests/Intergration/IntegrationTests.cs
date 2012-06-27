using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PocoHttp.Tests.Model;
using PocoHttp.Tests.Tools;

namespace PocoHttp.Tests.Intergration
{
	[TestFixture]
	public class IntegrationTests
	{
		private PocoClient _pocoClient;
		private Uri _requestUri;

		[SetUp]
		public void Setup()
		{
			var pocoConfiguration = new PocoConfiguration()
			                        	{
				Handler = new DummyServer(),
				ResponseReader = (a, b, c) => Task.Factory.StartNew(() =>
			                        		{
			                        			_requestUri =
			                        				a.RequestMessage.RequestUri;
			                        			return (object) new Car[0];
			                        		})
											
			                        	};

			pocoConfiguration.Handler = new DummyServer();
			_pocoClient = new PocoClient(pocoConfiguration)
			              	{
			              		BaseAddress = new Uri("http://non-existent-server/api/")
			              	};


		}

		[Test]
		[Ignore]
		public void GetCars()
		{
			var pocoClient = new PocoClient()
				{BaseAddress = new Uri("http://localhost:56572/api/")};
			pocoClient.Configuration.UsePluralUrls = true;
			var list = pocoClient.Context<Car>()
				.Where(x=>x.Make == "Fiat").OrderBy(x=>x.Price).ToList();
			Console.WriteLine(list[1].Model);
		}

		[Test]
		[Ignore]
		public void GetCars2()
		{
			var request = new HttpRequestMessage(HttpMethod.Get, "Cars");
			var httpClient = new HttpClient()
			                 	{
			                 		BaseAddress = new Uri("http://localhost:56572/api/")
			                 	};
			string result = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
			Console.WriteLine(result);
		}

		[Test]
		public void ODataGrammar_Test()
		{
			var queryable = _pocoClient.Context<Car>().Take(20).ToList();
			Assert.AreEqual(
				"http://non-existent-server/api/Cars?$top=20",
				_requestUri.ToString());

		}
	}
}
