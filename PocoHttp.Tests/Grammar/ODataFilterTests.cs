using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NUnit.Framework;
using PocoHttp.Internal;
using PocoHttp.Tests.Model;

namespace PocoHttp.Tests.Grammar
{
	[TestFixture]
	public class ODataFilterTests
	{
		[Test]
		public void FilterTest_string()
		{
			var oDataVisitor = new ODataVisitor();
			string name1 = "ali";
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Where(x => x.Make == "Fiat");

			var result = oDataVisitor.Translate(expression);
			Console.WriteLine(result);
			Assert.AreEqual("$filter=(Make eq 'Fiat')", result);
		}

		[Test]
		public void FilterTest_AND()
		{
			var oDataVisitor = new ODataVisitor();
			string name1 = "ali";
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Where(x => x.Make == "Fiat" & x.MaxSpeed == 130);

			var result = oDataVisitor.Translate(expression);
			Console.WriteLine(result);
			Assert.AreEqual("$filter=((Make eq 'Fiat') AND (MaxSpeed eq 130))", result);
		}

		[Test]
		public void FilterTest_ORELSE()
		{
			var oDataVisitor = new ODataVisitor();
			string name1 = "ali";
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Where(x => x.Make == "Fiat" || x.MaxSpeed == 130);

			var result = oDataVisitor.Translate(expression);
			Console.WriteLine(result);
			Assert.AreEqual("$filter=((Make eq 'Fiat') OR (MaxSpeed eq 130))", result);
		}


	}
}
