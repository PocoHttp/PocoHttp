using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using NUnit.Framework;
using PocoHttp.Internal.LinqHelper;

namespace PocoHttp.Tests
{
	public class ModelV
	{
		public string Name { get; set; }
	}

	[TestFixture]
	public class GeneralTests
	{


		[Test]
		public void TestReferrences()
		{
			var httpClient = new HttpClient();
			var queryTranslator = new QueryTranslator();
			string name1 = "ali";
			Expression<Func<IQueryable<ModelV>, IEnumerable<ModelV>>> expression = (IQueryable<ModelV> data) =>
				data.Where(x => x.Name == name1);
			var translate = queryTranslator.Translate(expression);
			Console.WriteLine(translate);
		}
	}
}
