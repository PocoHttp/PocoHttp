using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using PocoHttp.Grammars;
using PocoHttp.Routing;

namespace PocoHttp
{
	public class PocoRuntime : IPocoRuntime
	{
		private static readonly PocoRuntime _current = new PocoRuntime();

		private PocoRuntime()
		{
			Grammar = new ODataGrammar();
			UsePluralUrls = true;
			UriBuilder = new EntityUriBuilder();
			RequestSetup = (request) => {};
			DisposeHandler = true;
			CustomFormatters = new MediaTypeFormatter[0];
		}

		public static PocoRuntime Current
		{
			get { return _current; }
		}

		public IHttpDataGrammar Grammar {get; set; }

		public bool UsePluralUrls { get; set; }

		public IEntityUriBuilder UriBuilder { get; set; }

		public Action<HttpRequestMessage> RequestSetup { get; set; }

		public HttpMessageHandler Handler { get; set; }

		public bool DisposeHandler { get; set; }

		public IEnumerable<MediaTypeFormatter> CustomFormatters { get; set; }
		
		public Func<Uri, HttpRequestMessage> RequestBuilder { get; set; }
		
	}
}
