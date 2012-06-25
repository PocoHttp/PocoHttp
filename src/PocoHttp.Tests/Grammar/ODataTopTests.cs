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
	public class ODataTopTests
	{
		[Test]
		public void TopTest()
		{
			var oDataVisitor = new ODataVisitor();
			int count = 50;
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Take(50);

			var result = oDataVisitor.Translate(expression);
			Console.WriteLine(result);
			Assert.AreEqual("$top=50", result);
		}
	}
}
