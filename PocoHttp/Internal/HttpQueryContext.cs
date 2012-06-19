using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using PocoHttp.Grammars;

namespace PocoHttp.Internal
{
	internal class HttpQueryContext
	{
		public IPocoRuntime Runtime { get; set; }

		public Type EntityType { get; set; }

		public HttpRequestMessage Request { get; set; }

		public HttpClient HttpClient { get; set; }

	}
}
