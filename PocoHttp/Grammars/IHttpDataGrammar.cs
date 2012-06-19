using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;

namespace PocoHttp.Grammars
{
	public interface IHttpDataGrammar
	{
		void Compose(Expression expression, HttpRequestMessage message);
		string GetQueryText(Expression expression);
	}
}
