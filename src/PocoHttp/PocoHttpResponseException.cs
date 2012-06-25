using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PocoHttp
{
	public class PocoHttpResponseException : Exception
	{

		private readonly HttpResponseHeaders _responseHeaders;
		private readonly HttpStatusCode _statusCode;
		private readonly byte[] _content;

		public PocoHttpResponseException() :base()
		{
			
		}

		public PocoHttpResponseException(string message) : base(message)
		{
			
		}

		public PocoHttpResponseException(string message, Exception inner) : base(message, inner)
		{
			
		}

		public PocoHttpResponseException(HttpResponseMessage response)
			:this(string.Format("Status {0}", response.StatusCode))
		{
			_responseHeaders = response.Headers;
			_statusCode = response.StatusCode;
			try
			{
				if (response.Content != null)
					_content = response.Content.ReadAsByteArrayAsync().Result;
			}
			catch
			{
				// swallow
			}
		}

		

		public HttpResponseHeaders ResponseHeaders
		{
			get { return _responseHeaders; }
		}

		public HttpStatusCode StatusCode
		{
			get { return _statusCode; }
		}
	}
}
