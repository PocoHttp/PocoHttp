using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using NUnit.Framework;
using PocoHttp.Routing;

namespace PocoHttp.Tests.Routing
{
	public class UriBuilderTests
	{
		[TestCase(typeof(DummyEntity), false, "DummyEntity")]
		[TestCase(typeof(DummyEntity), true, "DummyEntitys")]
		[TestCase(typeof(AttributedDummyEntity), true, "/AnotherRoute/AttributedDummyEntity")]
		[TestCase(typeof(AttributedDummyEntity), false, "/AnotherRoute/AttributedDummyEntity")]
		public static void Test(Type type, bool usePlural, string expectedUri)
		{
			var entityUriBuilder = new EntityUriBuilder();
			var uri = entityUriBuilder.BuildUri(type, usePlural);
			Assert.AreEqual(expectedUri, uri.ToString());
		}

		[TestCase()]
		public static void UriTest()
		{
			var uri = new Uri("http://localhost:56572/api/");
			uri = new Uri(uri, "Cars");
			Assert.AreEqual(uri.ToString(), "http://localhost:56572/api/Cars");
		}

	}

	class DummyEntity
	{
		
	}

	[EntityUri("/AnotherRoute/AttributedDummyEntity")]
	class AttributedDummyEntity
	{
		
	}
}
