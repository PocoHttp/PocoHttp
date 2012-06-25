using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NUnit.Framework;
using PocoHttp.Grammars;
using PocoHttp.Internal;
using PocoHttp.Tests.Model;

namespace PocoHttp.Tests.Grammar
{
	[TestFixture]
	public class ODataGrammarTests
	{
		[Test]
		public void ComplexTest()
		{
			var oDataVisitor = new ODataVisitor();
			int count = 50;
			int skip = 20;
			string model = "X3";
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Where(x=>x.Model == model).Skip(skip).Take(count);

			var result = oDataVisitor.Translate(expression);
			Console.WriteLine(result);
			Assert.AreEqual("$filter=(Model eq 'X3')&$skip=20&$top=50", result);
		}

		[Test]
		public void ComplexTest_withOrdering_Normal()
		{
			var oDataVisitor = new ODataVisitor();
			int count = 50;
			int skip = 20;
			string model = "X3";
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Where(x => x.Model == model)
				.Skip(skip)
				.OrderBy(x=>x.Price);

			var result = oDataVisitor.Translate(expression);
			Console.WriteLine(result);
			Assert.AreEqual("$filter=(Model eq 'X3')&$skip=20&$orderby=Price", result);
		}

		[Test]
		public void ComplexTest_withOrdering_desc()
		{
			var oDataVisitor = new ODataVisitor();
			int count = 50;
			int skip = 20;
			string model = "X3";
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Where(x => x.Model == model)
				.Skip(skip)
				.OrderByDescending(x => x.Price);

			var result = oDataVisitor.Translate(expression);
			Console.WriteLine(result);
			Assert.AreEqual("$filter=(Model eq 'X3')&$skip=20&$orderby=Price DESC", result);
		}

		[Test]
		public void ComplexTest_With_Substring()
		{
			var oDataVisitor = new ODataVisitor();
			string model = "X";
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Where(x => x.Model.Substring(0,1) == model);

			var result = oDataVisitor.Translate(expression);
			Console.WriteLine(result);
			Assert.AreEqual("$filter=(substring(Model,0,1) eq 'X')", result);
		}
	}
}
