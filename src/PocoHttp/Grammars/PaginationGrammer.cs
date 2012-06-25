using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PocoHttp.Internal;

namespace PocoHttp.Grammars
{
	/// <summary>
	/// Provides a simple (yet REST-Frindlier) querying
	/// 
	/// </summary>
	public class PaginationGrammer : QueryStringGrammar
	{
		private readonly PaginationVocabulary _vocabulary;

		public PaginationGrammer() : this(new PaginationVocabulary())
		{
			
		}

		public PaginationGrammer(PaginationVocabulary vocabulary)
		{
			_vocabulary = vocabulary;
		}

		protected override string GetQueryString(Expression expression)
		{
			var odataVisitor = new PaginationVisitor(_vocabulary);
			return odataVisitor.Translate(expression);
		}
	}
}
