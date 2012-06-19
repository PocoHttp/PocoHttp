using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;

namespace PocoHttp.Grammars
{
	public abstract class QueryStringGrammar : IHttpDataGrammar
	{
		public void Compose(Expression expression, HttpRequestMessage request)
		{
			var queryString = GetQueryString(expression);
			
			Trace.WriteLine(queryString);

			request.RequestUri = new Uri(request.RequestUri.ToString() + 
				((request.RequestUri.ToString().IndexOf("?") >= 0) ? "&" : "?")
				+ queryString, request.RequestUri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
		
		}

		public string GetQueryText(Expression expression)
		{
			return GetQueryString(expression);
		}

		protected abstract string GetQueryString(Expression expression);
	}
}
