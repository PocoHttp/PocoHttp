using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using PocoHttp.Grammars;
using PocoHttp.Internal.LinqHelper;

namespace PocoHttp.Internal
{
	internal class HttpProvider : QueryProvider
	{
		private readonly HttpQueryContext _context;


		public HttpProvider(HttpQueryContext context)
		{
			_context = context;
		}

		public override string GetQueryText(Expression expression)
		{
			return _context.Runtime.Grammar.GetQueryText(expression);
		}

		public override object Execute(Expression expression)
		{
			_context.Runtime.Grammar.Compose(expression, _context.Request);
			if (_context.Runtime.RequestSetup != null)
				_context.Runtime.RequestSetup(_context.Request);

			var response = _context.HttpClient.SendAsync(_context.Request).Result;
			Task<object> task = null;
			var formatters = _context.Runtime.CustomFormatters;
 			if(formatters == null || formatters.Count()==0)
 				formatters = new MediaTypeFormatterCollection();

			if(response.StatusCode!= HttpStatusCode.OK)
			{
				throw new PocoHttpResponseException(response);
			}

			task = response.Content.ReadAsAsync(
				typeof(IEnumerable<>).MakeGenericType(_context.EntityType), 
				formatters);

			return task.Result;
		}
	}
}
