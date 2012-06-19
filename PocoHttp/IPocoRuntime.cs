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
	public interface IPocoRuntime
	{
		/// <summary>
		/// Http Data grammar.
		/// </summary>
		IHttpDataGrammar Grammar { get; set; }

		/// <summary>
		/// Whether use for example http://myserver/api/car or http://myserver/api/cars
		/// to retrieve Car objects
		/// </summary>
		bool UsePluralUrls { get; set; }

		IEntityUriBuilder UriBuilder { get; set; }

		/// <summary>
		/// And extensibility point for extra setup required
		/// </summary>
		Action<HttpRequestMessage> RequestSetup { get; set; }

		/// <summary>
		/// Handler to be used in HttpClient
		/// </summary>
		HttpMessageHandler Handler { get; set; }

		/// <summary>
		/// Whether handler should be disposed with HttpClieny
		/// </summary>
		bool DisposeHandler { get; set; }

		IEnumerable<MediaTypeFormatter> CustomFormatters { get; set; }

	}
}
