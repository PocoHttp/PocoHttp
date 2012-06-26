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
			return _context.Configuration.Grammar.GetQueryText(expression);
		}

		public override object Execute(Expression expression)
		{
			_context.Configuration.Grammar.Compose(expression, _context.Request);
			Trace.WriteLine(_context.Request.RequestUri.ToString());

			if (_context.Configuration.RequestSetup != null)
				_context.Configuration.RequestSetup(_context.Request);

			var response = _context.HttpClient.SendAsync(_context.Request).Result;
			Task<object> task = null;
			var formatters = _context.Configuration.CustomFormatters;
 			if(formatters == null || formatters.Count()==0)
 				formatters = new MediaTypeFormatterCollection();

			if(response.StatusCode!= HttpStatusCode.OK)
			{
				throw new PocoHttpResponseException(response);
			}

			task = _context.Configuration.ResponseReader(response, formatters, _context.EntityType);

			return task.Result;
		}
	}
}
