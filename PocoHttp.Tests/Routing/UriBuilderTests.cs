using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PocoHttp.Routing;

namespace PocoHttp.Tests.Routing
{
	public class UriBuilderTests
	{
		[TestCase(typeof(DummyEntity), false, "/DummyEntity")]
		[TestCase(typeof(DummyEntity), true, "/DummyEntitys")]
		[TestCase(typeof(AttributedDummyEntity), true, "/AnotherRoute/AttributedDummyEntity")]
		[TestCase(typeof(AttributedDummyEntity), false, "/AnotherRoute/AttributedDummyEntity")]
		public static void Test(Type type, bool usePlural, string expectedUri)
		{
			var entityUriBuilder = new EntityUriBuilder();
			var uri = entityUriBuilder.BuildUri(type, usePlural);
			Assert.AreEqual(expectedUri, uri.ToString());
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
