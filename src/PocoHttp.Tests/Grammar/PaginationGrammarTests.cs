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
	public class PaginationGrammarTests
	{
		[Test]
		public void SkipCount_default_vocabulary()
		{
			var paginationVisitor = new PaginationVisitor();
			int count = 50;
			int skip = 20;
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Skip(skip)
				.Take(count);

			var result = paginationVisitor.Translate(expression);
			Console.WriteLine(result);
			Assert.AreEqual("skip=20&count=50", result);
		}

		[Test]
		public void SkipCount_default_vocabulary_Different_Order()
		{
			var paginationVisitor = new PaginationVisitor();
			int count = 50;
			int skip = 20;
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Take(count).Skip(skip);

			var result = paginationVisitor.Translate(expression);
			Console.WriteLine(result);
			Assert.AreEqual("count=50&skip=20", result);
		}

		[Test]
		public void SkipCount_WithVocabulary()
		{
			var paginationVisitor = new PaginationVisitor(
				new PaginationVocabulary()
					{
						Count = "count1",
						Skip = "skip1"
					});
			int count = 50;
			int skip = 20;
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Skip(skip)
				.Take(count);

			var result = paginationVisitor.Translate(expression);
			Console.WriteLine(result);
			Assert.AreEqual("skip1=20&count1=50", result);
		}

		[Test]
		[ExpectedException(typeof(NotSupportedException))]
		public void Where_NotSupported()
		{
			var paginationVisitor = new PaginationVisitor(
				new PaginationVocabulary()
				{
					Count = "count1",
					Skip = "skip1"
				});
		
			Expression<Func<IQueryable<Car>, IEnumerable<Car>>> expression = (IQueryable<Car> data) =>
				data.Where(x=>x.Model == "Midel");

			var result = paginationVisitor.Translate(expression);
			Console.WriteLine(result);
		}

	}
}
