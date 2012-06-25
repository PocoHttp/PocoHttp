using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PocoHttp.Internal;

namespace PocoHttp.Grammars
{
	public class ODataGrammar : QueryStringGrammar
	{
		

		public ODataGrammar()
		{
			
		}

		protected override string GetQueryString(Expression expression)
		{
			var odataVisitor = new ODataVisitor();
			return odataVisitor.Translate(expression);
		}
	}
}
