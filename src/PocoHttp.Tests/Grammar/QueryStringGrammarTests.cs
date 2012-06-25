using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using NUnit.Framework;
using PocoHttp.Grammars;

namespace PocoHttp.Tests.Grammar
{
	public static class QueryStringGrammarTests
	{
		[TestCase("ali=bibi", "/api/jiji", "/api/jiji?ali=bibi")]
		[TestCase("ali=bibi", "/api/jiji?chi=ha", "/api/jiji?chi=ha&ali=bibi")]
		public static void Test(string value, string uri, string expectedValue)
		{
			var dummyGrammar = new DummyGrammar(value);
			var request = new HttpRequestMessage(HttpMethod.Get, new Uri( uri, UriKind.Relative));
			dummyGrammar.Compose(null, request);
			Assert.IsTrue(request.RequestUri.ToString().EndsWith(expectedValue), request.RequestUri.ToString());
		}
	}

	internal class DummyGrammar : QueryStringGrammar
 	{
		private string _value;

		public DummyGrammar(string value)
		{
			_value = value;
		}

		protected override string GetQueryString(Expression expression)
		{
			return _value;
		}
 	}
}
